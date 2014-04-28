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
using System.Reflection;
using TISFAT_ZERO.Scripts;

namespace TISFAT_ZERO
{
	public partial class Canvas : Form
	{

		#region Variables
		public Color bkgColor;

		public static List<LightObject> lights = new List<LightObject>();
		public readonly float lightSize = 400.0f;
		public static bool renderShadows = false;

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
			if (selectedJoint == null)
				return;

			//Set the cursor position in the toolbox
			Program.ToolboxForm.lbl_xPos.Text = "X Pos: " + e.X.ToString();
			Program.ToolboxForm.lbl_yPos.Text = "Y Pos: " + e.Y.ToString();

			//If the canvas is to be drawn, and the user isn't holding down the right mouse button
			//This is mostly so that you won't be dragging the entire figure at the same time
			//That you're dragging a joint
			if (draw & !(e.Button == MouseButtons.Right))
			{
				//To prevent exceptions being thrown. 
				if (selectedJoint != null)
				{
					if (selectedJoint.ParentFigure != null)
						if (!(selectedJoint.ParentFigure.type == 3 || selectedJoint.ParentFigure.type == 6))
						{
							selectedJoint.SetPos(e.X, e.Y);
							Program.ToolboxForm.lbl_dbgAngleToParent.Text = "AngleToParent: " + selectedJoint.AngleToParent;
							Refresh();
						}
						else
						{
							selectedJoint.SetPosAbs(e.X, e.Y);

							if (selectedJoint.ParentFigure.type == 3)
								((StickRect)selectedJoint.ParentFigure).onRectJointMoved(selectedJoint);
						}
				}

				//This prevents any other figures from becoming active as you are dragging a joint.
				//Deprecated for now, possibly of some use in the future.

				/*
				foreach (StickObject fig in figureList)
				{
					if (!(fig == activeFigure))
						fig.isActiveFig = false;
				}
				*/
			}
			else if (draw & e.Button == MouseButtons.Right)
			{
				//This prevents the context menu from popping up after you release the right
				//mouse button when you're dragging a figure.
				mousemoved = true;

				//This basically keeps the distance from the cursor and the figure constant
				//as the user drags it around.
				for (int i = 0;i < activeFigure.Joints.Count;i++)
				{
					activeFigure.Joints[i].location.X = fx[i] + (e.X - ox);
					activeFigure.Joints[i].location.Y = fy[i] + (e.Y - oy);
				}

				//Refresh the canvas or the user won't see any difference.
				Refresh();
			}

			//This is what sets the active figure to whatever figure owns the joint that you
			//moused over. I'm pretty sure that activeFigure is deprecated because multiple figures can't be on the same 
			//layer anymore (for now).
			for (int i = 0;i < figureList.Count;i++)
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
		/// <param name="sender">The source of the event.</param>y
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
					Program.ToolboxForm.updateBitmapList();

					//This sets the labels in the debug menu.
					if (selectedJoint != null)
					{
						Program.ToolboxForm.lbl_selectedJoint.Text = "Selected Joint: " + f.name;
						Program.ToolboxForm.lbl_jointLength.Text = "Joint Length: " + f.CalcLength(null).ToString();
						Program.ToolboxForm.lbl_dbgAngleToParent.Text = "AngleToParent: " + f.AngleToParent;
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
							f.state = (f.state == 1) ? f.state = 0 : f.state = 1;
							hasLockedJoint = !hasLockedJoint;
							GL_GRAPHICS.Invalidate();
							selectedJoint = f;
							Program.ToolboxForm.updateBitmapList();
							activeFigure.setAsBase(f);
						}
					}
					catch
					{
						return;
					}
				}
			}
			if (e.Button == MouseButtons.Right)
			{
				if (activeFigure == null)
					return;

				ox = e.X;
				oy = e.Y;
				fx = new List<int>();
				fy = new List<int>();

				for (int i = 0;i < activeFigure.Joints.Count;i++)
				{
					fx.Add(activeFigure.Joints[i].location.X);
					fy.Add(activeFigure.Joints[i].location.Y);
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

		#region Figures
		/// <summary>
		/// Adds the figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void addFigure(StickObject figure) { figureList.Add(figure); }

		/// <summary>
		/// Adds the tween figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void addTweenFigure(StickObject figure) { tweenFigs.Add(figure); figure.isTweenFig = true; }

		/// <summary>
		/// Removes the figure.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void removeFigure(StickObject figure) { figureList.Remove(figure); }

		/// <summary>
		/// Removes the specified tween figure from the tween figures list.
		/// </summary>
		/// <param name="figure">The figure.</param>
		public static void removeTweenFigure(StickObject figure) { tweenFigs.Remove(figure); }

		/// <summary>
		/// Activates the figure.
		/// </summary>
		/// <param name="fig">The fig.</param>
		public static void activateFigure(StickObject fig)
		{
			for (int i = 0;i < figureList.Count;i++)
				figureList[i].isActiveFig = false;

			fig.isActiveFig = true;
		}
		#endregion

		#region Right Click Menu
		private void flipArmsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipArms();
			Refresh();
		}

		private void flipLegsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (activeFigure.type == 1)
				((StickFigure)activeFigure).flipLegs();
			Refresh();
		}
		#endregion

		private int GL_Create_TextureID(string fileName)
		{
			Bitmap raw = new Bitmap(fileName);
			BitmapData rawData = raw.LockBits(new Rectangle(0, 0, raw.Width, raw.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			int gl_id = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, gl_id);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, rawData.Width, rawData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, rawData.Scan0);
			raw.UnlockBits(rawData);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

			GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

			return gl_id;
		}

		private void Canvas_Load(object sender, EventArgs e)
		{
			glGraphics = GL_GRAPHICS;
			glGraphics.MakeCurrent();

			bkgColor = Color.White;

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
			GL.ClearColor(bkgColor);

			//Since we are 2d, we don't need the depth test
			GL.Disable(EnableCap.DepthTest);

			#region Shader Initialization Stuff
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
			#endregion

			//lights.Add(new Point(100, 250));
			//lights.Add(new Point(200, 200));
			//lights.Add(new Point(300, 250));

			Program.TimelineForm.addStickLayer("Stick Layer 1");
		}

		public void setBackgroundColor(Color c)
		{
			bkgColor = c;

			GL.ClearColor(c);
			GL_GRAPHICS.Invalidate();
		}

		private void createOccluderMap(Point pos)
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, OccluderFBO);

			GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.ColorMask(false, false, false, true);

			GL.PushMatrix();

			GL.Translate(-pos.X, -(GL_HEIGHT - pos.Y - (int)lightSize), 0.0);

			for (int i = figureList.Count;i > 0;i--)
				figureList[i - 1].drawFigure();

			for (int i = tweenFigs.Count;i > 0;i--)
				tweenFigs[i - 1].drawFigure();

			GL.PopMatrix();

			GL.ColorMask(true, true, true, true);

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

			GL.ClearColor(bkgColor);
		}

		private void DrawShadows(Point pos)
		{
			Drawing.ReadyDraw(glGraphics);

			Drawing.DrawGraphics(5, Color.FromArgb(255, 0, 255, 255), new Point(pos.X - 3, pos.Y - 3), 0, 0, new Point(pos.X + 3, pos.Y + 3));

			createOccluderMap(new Point(pos.X - (int)lightSize / 2, pos.Y - (int)lightSize / 2));

			GL.UseProgram(Program_passAndMap);

			GL.Uniform1(GL.GetUniformLocation(Program_passAndMap, "u_s2DTexture"), 0);
			GL.Uniform2(GL.GetUniformLocation(Program_passAndMap, "u_vResolution"), new Vector2(lightSize, lightSize));

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, ShadowsFBO);

			GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
			GL.Clear(ClearBufferMask.ColorBufferBit);

			GL.PushMatrix();

			GL.Translate(-pos.X, -(GL_HEIGHT - pos.Y - (int)lightSize) - ((int)lightSize - 1), 0.0);
			Drawing.DrawGraphics(6, Color.FromArgb(255, 255, 255, 255), new Point(pos.X, pos.Y), (int)lightSize, 1, pos, (int)OccludersTexture);

			GL.PopMatrix();

			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

			GL.UseProgram(0);

			GL.ClearColor(bkgColor);

			GL.UseProgram(Program_passAndRender);

			GL.Uniform1(GL.GetUniformLocation(Program_passAndRender, "u_s2DTexture"), 0);
			GL.Uniform2(GL.GetUniformLocation(Program_passAndRender, "u_vResolution"), new Vector2(lightSize, lightSize));

			Drawing.DrawGraphics(6, Color.FromArgb(255, 255, 255, 255), new Point(pos.X - (int)lightSize / 2, (pos.Y - (int)lightSize / 2)), (int)lightSize, (int)lightSize, pos, (int)ShadowsTexture);

			GL.UseProgram(0);
		}

		private void GL_GRAPHICS_OnRender(object sender, EventArgs e)
		{
			if (!GLLoaded)
				return;

			//Todo: make a better rendering loop
			GL_GRAPHICS.Invalidate();
		}

		private void GL_GRAPHICS_Paint(object sender, PaintEventArgs e)
		{
			//Reverse the figure / tweenfigure list.
			figureList.Reverse();
			tweenFigs.Reverse();

			if (!GLLoaded)
				return;

			GL_GRAPHICS.MakeCurrent();

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.StencilBufferBit);

			if (Canvas.renderShadows)
				for (int i = lights.Count;i > 0;i--)
				{
					if (lights[i - 1].isTweenFig)
					{
						if (lights[i - 1].drawFig)
						{
							DrawShadows(lights[i - 1].Joints[0].location);
						}
					}
					else
						DrawShadows(lights[i - 1].Joints[0].location);
				}

			for (int i = figureList.Count;i > 0;i--)
				figureList[i - 1].drawFigure();

			for (int i = tweenFigs.Count;i > 0;i--)
				tweenFigs[i - 1].drawFigure();

			for (int i = figureList.Count;i > 0;i--)
				figureList[i - 1].drawFigHandles();

			//Re-reverse the figure / tweenfigure list.
			figureList.Reverse();
			tweenFigs.Reverse();

			GL_GRAPHICS.SwapBuffers();
		}

		//"shader" being a shader inside the /shaders folder
		//^^Above is obsolete; Shaders are now an embedded resource. -Evar
		private string readShader(string shader)
		{
			Assembly _assembly = Assembly.GetExecutingAssembly();
			foreach (string s in _assembly.GetManifestResourceNames())
				Console.WriteLine(s);

			StreamReader _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("TISFAT_ZERO.Shaders." + shader));

			return _textStreamReader.ReadToEnd();

			//return File.ReadAllText(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.IO.Path.GetFullPath(shader)))) + "\\Shaders\\" + shader);
		}

		public void recieveStickFigure(StickCustom figure)
		{
			CustomLayer c = (CustomLayer)Timeline.layers[Timeline.layer_sel];

			List<StickJoint> ps = figure.Joints;
			c.fig = figure;

			int[] positions = new int[ps.Count];
			for (int a = 0;a < ps.Count;a++)
			{
				StickJoint p = ps[a].parent;
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
			c.keyFrames[1].Joints = custObjectFrame.createClone(ps);
			foreach (StickJoint j in c.keyFrames[1].Joints)
				j.ParentFigure = c.fig;

			c.tweenFig = new StickCustom(figure, true);
			c.tweenFig.Joints = custObjectFrame.createClone(ps);
			foreach (StickJoint j in c.tweenFig.Joints)
				j.ParentFigure = c.tweenFig;

			addFigure(c.fig);
			Timeline.layer_sel = Timeline.layer_cnt - 1;
			Program.TimelineForm.setFrame(c.firstKF);
			Program.TimelineForm.Invalidate();
		}

		public void recieveStickFigure(StickCustom figure, bool lean)
		{
			/*
			CustomLayer c = (CustomLayer)Timeline.layers[Timeline.layer_sel];

			List<StickJoint> ps = figure.Joints;
			c.fig = figure;

			int[] positions = new int[ps.Count];
			for (int a = 0;a < ps.Count;a++)
			{
				StickJoint p = ps[a].parent;
				if (p != null)
				{
					positions[a] = ps.IndexOf(p);
				}
				else
				{
					positions[a] = -1;
				}
			}

			c.keyFrames[c.selectedFrame].Joints = ps;
			foreach (StickJoint j in c.keyFrames[c.selectedFrame].Joints)
				j.ParentFigure = c.fig;

			c.tweenFig = new StickCustom(figure, true);
			c.tweenFig.Joints = custObjectFrame.createClone(ps);
			foreach (StickJoint j in c.tweenFig.Joints)
				j.ParentFigure = c.tweenFig;

			Timeline.layer_sel = Timeline.layer_cnt - 1;
			Program.TimelineForm.setFrame(c.firstKF);
			Program.TimelineForm.Invalidate();
			 */
		}

		private void GL_GRAPHICS_Resize(object sender, EventArgs e)
		{
			GL_HEIGHT = GL_GRAPHICS.Height;
			GL_WIDTH = GL_GRAPHICS.Width;
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();

			GL.Viewport(0, 0, GL_WIDTH, GL_HEIGHT);
			GL.Ortho(0, GL_WIDTH, 0, GL_HEIGHT, -1, 1);
			GL.ClearColor(bkgColor);
		}
	}
}