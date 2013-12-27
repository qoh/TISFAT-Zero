using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;

namespace TISFAT_Zero
{
	partial class Canvas : Form, ICanDraw
	{

		#region Variables
        private static List<Point> lights = new List<Point>();
        public readonly float lightSize = 400.0f;

        public static Graphics theCanvasGraphics; //We need a list of objects to draw.
        public static bool GLLoaded = false; //we can't touch GL until its fully loaded, this is a guard variable
        public static int GL_WIDTH, GL_HEIGHT;

        private static int maxaa;

        public static List<StickObject> figureList = new List<StickObject>();
        public static List<StickObject> tweenFigs = new List<StickObject>();
        public static List<int> textures = new List<int>();

        //Now we need a method to add figures to these lists.

        public static StickObject activeFigure;
        public static StickJoint selectedJoint = new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);

        public bool mousemoved, draw, hasLockedJoint = false;
        private int ox, oy;

		private uint OccludersTexture, ShadowsTexture, OccluderFBO, ShadowsFBO;

        private int Shader_pass, Shader_shadowMap, Shader_shadowRender, Program_passAndMap, Program_passAndRender;

        private List<int> fx, fy;

        public OpenTK.GLControl glGraphics;

		public GLControl GLGraphics
		{
			get { return glGraphics; }
		}

		#endregion

		//Instantiate the class
		/// <summary>
		/// Initializes a new instance of the <see cref="Canvas"/> class.
		/// </summary>
		/// <param name="f">The main form object</param>
		/// <param name="t">The toolbox object</param>
		public Canvas()
		{
			int aa = 0;

			do
			{
				var mode = new GraphicsMode(32, 0, 0, aa);
				if (mode.Samples == aa)
					maxaa = aa;
				aa += 2;
			} while (aa <= 32);

			InitializeComponent();
		}

		#region Mouse Events
		//Note: These events are hooked with the GLControl and not the canvas
		//Debug stuff, and dragging joints.
		/// <summary>
		/// Handles the MouseMove event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Canvas_MouseMove(object sender, MouseEventArgs e)
		{
			//Set the cursor position in the toolbox
			//Program.TheToolbox.lbl_xPos.Text = "X Pos: " + e.X.ToString();
			//Program.TheToolbox.lbl_yPos.Text = "Y Pos: " + e.Y.ToString();

			//If the canvas is to be drawn, and the user isn't holding down the right mouse button
			//This is mostly so that you won't be dragging the entire figure at the same time
			//That you're dragging a joint
			if (draw & !(e.Button == MouseButtons.Right))
			{
				//To prevent exceptions being thrown.
				if (!(selectedJoint == null) && (selectedJoint.parentFigure.figureType != 3))
				{
					selectedJoint.setPos(e.X, e.Y);
					Refresh();
				}
				else if (selectedJoint != null && selectedJoint.parentFigure.figureType == 3)
				{
					selectedJoint.SetPosAbs(e.X, e.Y);
					((StickRect)selectedJoint.parentFigure).onRectJointMoved(selectedJoint);
				}

				//This prevents any other figures from becoming active as you are dragging a joint.
				foreach (StickObject fig in figureList)
				{
					if (!(fig == activeFigure))
						fig.isActiveFig = false;
				}
			}
			else if (draw & e.Button == MouseButtons.Right)
			{
				//This prevents the context menu from popping up after you release the right
				//mouse button when you're dragging a figure.
				mousemoved = true;

				//This basically keeps the distance from the cursor and the figure constant
				//as the user drags it around.
				for (int i = 0; i < activeFigure.FigureJoints.Count; i++)
				{
					activeFigure.FigureJoints[i].location.X = fx[i] + (e.X - ox);
					activeFigure.FigureJoints[i].location.Y = fy[i] + (e.Y - oy);
				}

				//Refresh the canvas or the user won't see any difference.
				Refresh();
			}

			//This is what sets the active figure to whatever figure owns the joint that you
			//moused over.
			for (int i = 0; i < figureList.Count; i++)
			{
				if (figureList[i].getPointAt(new Point(e.X, e.Y), 6) != -1 && figureList[i].drawHandles)
				{
					if (activeFigure != null)
						activeFigure.isActiveFig = false;

					activeFigure = figureList[i];
					activeFigure.isActiveFig = true;

					Refresh();
					break;
				}
			}

			//If the active figure exists (isn't null), and we aren't supposed to redraw, then..
			//This is what sets the cursor to the hand when you mouse over a joint.
			if (!(activeFigure == null) & !draw)
			{
				if (activeFigure.getPointAt(new Point(e.X, e.Y), 6) != -1)
					this.Cursor = Cursors.Hand;
				else
					this.Cursor = Cursors.Default;
			}
		}

		//Debug stuff, and selection of joints. This also causes the canvas to be redrawn on mouse move.
		/// <summary>
		/// Handles the MouseDown event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Canvas_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				//If the user isn't holding down the 'ctrl' key..
				if (!(ModifierKeys == Keys.Control))
				{
					//This prevents a null reference exception when a user left clicks
					//without an activeFigure being set yet.
					if (activeFigure == null)
						return;

					StickJoint f;

					//Selects the point at the location that the user clicked, with a
					//tolerance of about 4 pixels.
					f = activeFigure.selectPoint(new Point(e.X, e.Y), 6);

					//if(!hasLockedJoint)
					//	activeFigure.setAsBase(activeFigure.Joints[(activeFigure.Joints.IndexOf(f) + 1) % activeFigure.Joints.Count]);

					//Sets the selectedJoint variable to the joint that we just selected.
					selectedJoint = f;

					//This sets the labels in the debug menu.
					if (selectedJoint != null)
					{
						//theToolbox.lbl_selectedJoint.Text = "Selected Joint: " + f.jointName;
						//theToolbox.lbl_jointLength.Text = "Joint Length: " + f.CalcLength(null).ToString();
					}

					//This tells the form that the mouse button is being held down, and
					//that we should redraw the form when it's moved.
					draw = true;
				}
				else if (ModifierKeys == Keys.Control)
				{
					try
					{
						StickJoint f = null;
						if (activeFigure != null)
						{
							f = activeFigure.selectPoint(new Point(e.X, e.Y), 6);
							f.jointState = (f.jointState == 1) ? f.jointState = 0 : f.jointState = 1;
							hasLockedJoint = !hasLockedJoint;
							GL_GRAPHICS.Invalidate();
							selectedJoint = f;
							activeFigure.setAsBase(f);
						}
					}
					catch
					{
						return;
					}
				}
			}
			else if (e.Button == MouseButtons.Right)
			{
				if (activeFigure == null)
					return;

				ox = e.X;
				oy = e.Y;
				fx = new List<int>();
				fy = new List<int>();

				for (int i = 0; i < activeFigure.FigureJoints.Count; i++)
				{
					fx.Add(activeFigure.FigureJoints[i].location.X);
					fy.Add(activeFigure.FigureJoints[i].location.Y);
				}

				draw = true;
			}
		}

		//Deselect the joint, and stop redrawing the canvas.
		/// <summary>
		/// Handles the MouseUp event of the Canvas control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
		private void Canvas_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				selectedJoint = new StickJoint("null", new Point(0, 0), 0, Color.Transparent, Color.Transparent);
				draw = false;
			}
			if (e.Button == MouseButtons.Right)
			{
				if (!mousemoved)
					contextMenuStrip1.Show(new Point(e.X + contextMenuStrip1.Height, e.Y + contextMenuStrip1.Width));
				mousemoved = false;
				draw = false;
			} 
		} 
		#endregion

		#region Graphics

		/// <summary>
		/// Draws the graphics.
		/// </summary>
		/// <param name="type">What we're drawing. 1 = Line, 1 = Circle, 2 = Handle, 3 = Hollow Handle</param>
		/// <param name="color">The <see cref="Color">color</see> of what we're drawing.</param>
		/// <param name="one">The origin point.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="two">The end point. (only used in line type)</param>
		public void drawGraphics(int type, Color color, Point one, int width, int height, Point two)
		{
			if (!GLLoaded)
				return;

			//Invert the y so OpenGL can draw it right-side up
			one.Y = GL_HEIGHT - one.Y;
			two.Y = GL_HEIGHT - two.Y;

			GL.Enable(EnableCap.Blend);
			GL.Enable(EnableCap.Multisample);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

			if (type == 0) //Line
			{
				//since some opengl cards don't support line widths past 1.0, we need to draw quads
				GL.Color4(color);

				//step 1: spam floats
				float x1 = one.X;
				float x2 = two.X;
				float y1 = one.Y;
				float y2 = two.Y;

				//step 2: get slope/delta
				float vecX = x1 - x2;
				float vecY = y1 - y2;

				//step 3: calculate distance
				float dist = (float)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));

				//step 4: normalize
				float norm1X = (vecX / dist);
				float norm1Y = (vecY / dist);

				GL.Begin(PrimitiveType.Quads);

				//step 5: get the perpindicular line to norm1, and scale it based on our width
				float normX = norm1Y * width / 2;
				float normY = -norm1X * width / 2;

				//step 6: draw the quad from the points using the normal as the offset
				GL.Vertex2((one.X - normX), (one.Y - normY));
				GL.Vertex2((one.X + normX), (one.Y + normY));

				GL.Vertex2((two.X + normX), (two.Y + normY));
				GL.Vertex2((two.X - normX), (two.Y - normY));

				GL.End();

				DrawCircle(one.X, one.Y, width / 2);
				DrawCircle(two.X, two.Y, width / 2);
			}
			else if (type == 1) //Circle
			{
				GL.Color4(color);
				DrawCircle(one.X, one.Y, width);
			}
			else if (type == 2) //Handle
			{
				//Evar doesn't like antialiased handles :c
				GL.Disable(EnableCap.Multisample);

				GL.Color4(color);
				GL.Begin(PrimitiveType.Quads);

				GL.Vertex2(one.X - 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y + 2.5);
				GL.Vertex2(one.X - 2.5, one.Y + 2.5);

				GL.End();

				GL.Enable(EnableCap.Multisample);
			}
			else if (type == 3) //Hollow Handle
			{
				GL.Disable(EnableCap.Multisample);

				GL.Color4(color);
				GL.Begin(PrimitiveType.LineLoop);

				GL.Vertex2(one.X - 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y - 2.5);
				GL.Vertex2(one.X + 2.5, one.Y + 2.5);
				GL.Vertex2(one.X - 2.5, one.Y + 2.5);

				GL.End();

				GL.Enable(EnableCap.Multisample);
			}
			else if (type == 5) //Rect Fill
			{
				GL.Color4(color);
				GL.Begin(PrimitiveType.Quads);

				GL.Vertex2(one.X, one.Y);
				GL.Vertex2(two.X, one.Y);
				GL.Vertex2(two.X, two.Y);
				GL.Vertex2(one.X, two.Y);

				GL.End();
			}
            else if (type == 6) //Texture
            {
                /*GL.Color4(color);

                GL.Enable(EnableCap.Texture2D);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

                GL.BindTexture(TextureTarget.Texture2D, textureID);

                GL.Begin(BeginMode.Quads);

                GL.TexCoord2(0.0, 1.0);
                GL.Vertex2(one.X, one.Y);

                GL.TexCoord2(0.0, 0.0);
                GL.Vertex2(one.X, one.Y - height);

                GL.TexCoord2(1.0, 0.0);
                GL.Vertex2(one.X + width, one.Y - height);

                GL.TexCoord2(1.0, 1.0);
                GL.Vertex2(one.X + width, one.Y);

                GL.End();

                GL.Disable(EnableCap.Blend);
                GL.Disable(EnableCap.Texture2D);*/
            }

			GL.Disable(EnableCap.Blend);
		} 

		private static void DrawCircle(float cx, float cy, float r) 
		{
			int num_segments = 6 * (int)Math.Sqrt(r);

			float theta = 6.28271f / num_segments;
			float tangetial_factor = (float)Math.Tan(theta);

			float radial_factor = (float)Math.Cos(theta);

			float y = 0; 
	
			GL.Begin(PrimitiveType.TriangleFan);

			for(int ii = 0; ii < num_segments; ii++) 
			{ 
				GL.Vertex2(r + cx, y + cy);

				float ty = r;

				r = (r + -y * tangetial_factor) * radial_factor;
				y = (y + ty * tangetial_factor) * radial_factor;
			}

			GL.End();
		}

		#endregion

		#region Figures

		/// <summary>
		/// Activates the figure.
		/// </summary>
		/// <param name="fig">The fig.</param>
		public static void activateFigure(StickObject fig)
		{
			for (int i = 0; i < figureList.Count; i++)
				figureList[i].isActiveFig = false;

			fig.isActiveFig = true;
		}
		#endregion

		#region Right Click Menu
		private void flipArmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.figureType == 1)
				((StickFigure)activeFigure).flipArms();
			Refresh();
		}

		private void flipLegsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.figureType == 1)
				((StickFigure)activeFigure).flipLegs();
			Refresh();
		} 
		#endregion

        private void Canvas_Load(object sender, EventArgs e)
        {
            glGraphics = GL_GRAPHICS;
            glGraphics.MakeCurrent();

            //GLControl's load event is never fired, so we have to piggyback off the canvas's load function instead
            GLLoaded = true;

            //If you are going to be resizing the canvas later or changing the background color,
            //make sure to re-do these so the GLControl will work properly
            GL_HEIGHT = GL_GRAPHICS.Height;
            GL_WIDTH = GL_GRAPHICS.Width;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Viewport(0, 0, GL_WIDTH, GL_HEIGHT);
            GL.Ortho(0, GL_WIDTH, 0, GL_HEIGHT, -1, 1);
            GL.ClearColor(Color.White);

            //Since we are 2d, we don't need the depth test
            GL.Disable(EnableCap.DepthTest);

			/*
            //We need to generate a texture here so we can write what we're drawing to the FBO, and then we can sample it with a shader
            GL.GenTextures(1, out OccludersTexture);
            GL.BindTexture(TextureTarget.Texture2D, OccludersTexture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);

            //make it the size of our lights
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)lightSize, (int)lightSize, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            //This texture will be used with the shadowMap
            GL.GenTextures(1, out ShadowsTexture);
            GL.BindTexture(TextureTarget.Texture2D, ShadowsTexture);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            //GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, (int)lightSize, 1, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            //Generate a FBO for writing to the sampling texture
            GL.GenFramebuffers(1, out OccluderFBO);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, OccluderFBO);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, OccludersTexture, 0);

            DrawBuffersEnum[] buf = new DrawBuffersEnum[1] { (DrawBuffersEnum)FramebufferAttachment.ColorAttachment0 };
            GL.DrawBuffers(buf.Length, buf);

            //and for our shadowMap, which is going to be 1xlightSize...
            GL.GenFramebuffers(1, out ShadowsFBO);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ShadowsFBO);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, ShadowsTexture, 0);

            DrawBuffersEnum[] bufferEnum = new DrawBuffersEnum[1] { (DrawBuffersEnum)FramebufferAttachment.ColorAttachment0 };
            GL.DrawBuffers(bufferEnum.Length, bufferEnum);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            //Create our shaders for drawing shadows
            Shader_pass = GL.CreateShader(ShaderType.VertexShader);
            Shader_shadowMap = GL.CreateShader(ShaderType.FragmentShader);
            Shader_shadowRender = GL.CreateShader(ShaderType.FragmentShader);

            Program_passAndMap = GL.CreateProgram();
            Program_passAndRender = GL.CreateProgram();

            //pass will fill in the needed variables that are not accessable in fragment shaders (but only in vertex shaders)
            //uniforms: u_projTrans
            GL.ShaderSource(Shader_pass, readShader("pass_vert.glsl"));
            GL.CompileShader(Shader_pass);

            int status;
            GL.GetShader(Shader_pass, ShaderParameter.CompileStatus, out status);

            if (status != 1)
                Console.WriteLine("Shader_pass failed to compile:\n" + GL.GetShaderInfoLog(Shader_pass));

            //shadowMap will "map" the shadows onto a 1D texture
            //uniforms: u_texture, resolution
            GL.ShaderSource(Shader_shadowMap, readShader("shadowMap_frag.glsl"));
            GL.CompileShader(Shader_shadowMap);

            GL.GetShader(Shader_shadowMap, ShaderParameter.CompileStatus, out status);

            if (status != 1)
                Console.WriteLine("Shader_shadowMap failed to compile:\n" + GL.GetShaderInfoLog(Shader_shadowMap));

            //and finally, shadowRender will take this map and render it onto the screen
            //uniforms: u_texture, resolution, softShadows
            GL.ShaderSource(Shader_shadowRender, readShader("shadowRender_frag.glsl"));
            GL.CompileShader(Shader_shadowRender);

            GL.GetShader(Shader_shadowRender, ShaderParameter.CompileStatus, out status);

            if (status != 1)
                Console.WriteLine("Shader_shadowRender failed to compile:\n" + GL.GetShaderInfoLog(Shader_shadowRender));

            GL.AttachShader(Program_passAndMap, Shader_pass);
            GL.AttachShader(Program_passAndMap, Shader_shadowMap);

            GL.AttachShader(Program_passAndRender, Shader_pass);
            GL.AttachShader(Program_passAndRender, Shader_shadowRender);

            GL.LinkProgram(Program_passAndMap);
            GL.LinkProgram(Program_passAndRender);

            //lights.Add(new Point(100, 250));
            lights.Add(new Point(200, 200));
            //lights.Add(new Point(300, 250)); */
        }

		public void setBackgroundColor(Color c)
		{
			GL.ClearColor(c);
			GL_GRAPHICS.Invalidate();
		}

        private void createOccluderMap(Point pos)
        {
			//Dunno what to do with this
			/*
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, OccluderFBO);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.ColorMask(false, false, false, true);

            GL.PushMatrix();

            GL.Translate(-pos.X, -(GL_HEIGHT - pos.Y - (int)lightSize), 0.0);

            for (int i = figureList.Count; i > 0; i--)
                figureList[i - 1].drawFigure();

            for (int i = tweenFigs.Count; i > 0; i--)
                tweenFigs[i - 1].drawFigure();

            GL.PopMatrix();

            GL.ColorMask(true, true, true, true);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.ClearColor(Color.DarkGray);
			 */
        }

        private void DrawShadows(Point pos)
        {
			/* Shaders stuff needs to be redone anyways...
            drawGraphics(5, Color.FromArgb(255, 0, 255, 255), new Point(pos.X - 3, pos.Y - 3), 0, 0, new Point(pos.X + 3, pos.Y + 3));

            createOccluderMap(new Point(pos.X - (int)lightSize / 2, pos.Y - (int)lightSize / 2));

            GL.UseProgram(Program_passAndMap);

            GL.Uniform1(GL.GetUniformLocation(Program_passAndMap, "u_s2DTexture"), 0);
            GL.Uniform2(GL.GetUniformLocation(Program_passAndMap, "u_vResolution"), new Vector2(lightSize, lightSize));

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, ShadowsFBO);

            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.PushMatrix();

            GL.Translate(-pos.X, -(GL_HEIGHT - pos.Y - (int)lightSize) - ((int)lightSize - 1), 0.0);
            drawGraphics(6, Color.FromArgb(255, 255, 255, 255), new Point(pos.X, pos.Y), (int)lightSize, 1, pos, (int)OccludersTexture);

            GL.PopMatrix();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.UseProgram(0);

            GL.ClearColor(Color.DarkGray);

            GL.UseProgram(Program_passAndRender);

            GL.Uniform1(GL.GetUniformLocation(Program_passAndRender, "u_s2DTexture"), 0);
            GL.Uniform2(GL.GetUniformLocation(Program_passAndRender, "u_vResolution"), new Vector2(lightSize, lightSize));

            drawGraphics(6, Color.FromArgb(255, 255, 255, 255), new Point(pos.X - (int)lightSize / 2, (pos.Y - (int)lightSize / 2)), (int)lightSize, (int)lightSize, pos, (int)ShadowsTexture);

            GL.UseProgram(0); */
        }

		private void GL_GRAPHICS_OnRender(object sender, EventArgs e)
		{
			if(!GLLoaded)
				return;

			//Todo: make a better rendering loop
			GL_GRAPHICS.Invalidate();
		}

        private void GL_GRAPHICS_Paint(object sender, PaintEventArgs e)
        {
            if (!GLLoaded)
                return;

			Timeline.doRender();
        }

        //"shader" being a shader inside the /shaders folder
        private string readShader(string shader)
        {
            return File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetFullPath(shader)))) + "/Shaders/" + shader);
        }

		public void recieveStickFigure(StickCustom figure)
		{
			/*
			CustomLayer c = (CustomLayer)(Program.TheTimeline.Layers[Program.TheTimeline.selectedLayer_Ind]);

			List<StickJoint> ps = figure.Joints;
			c.fig = figure;

			int[] positions = new int[ps.Count];
			for (int a = 0; a < ps.Count; a++)
			{
				StickJoint p = ps[a].parentJoint;
				if (p != null)
				{
					positions[a] = ps.IndexOf(p);
				}
				else
				{
					positions[a] = -1;
				}
			}

			c.keyFrames[0].Joints = ps;
			c.keyFrames[1].Joints = custObjectFrame.createClone(ps, positions);

			c.tweenFig = new StickCustom(true);
			c.tweenFig.Joints = custObjectFrame.createClone(ps, positions);

			addFigure(c.LayerFigure);
			Timeline.layer_sel = Timeline.Layers.Count - 1;
			Program.TheTimeline.setFrame(c.firstKF);
			Program.TheTimeline.Invalidate();*/
		}

		public void recieveStickFigure(StickCustom figure, bool lean)
		{
			//TODO: Make the current keyframe update with the new positions for the Custom Stick.
		}
	}
}