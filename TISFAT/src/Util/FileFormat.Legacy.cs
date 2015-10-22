using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util.Legacy.Structures;

// This stuff goes in this cage and NOBODY HAS PERMISSION TO LET IT OUT PLEASE NO.
// Code for legacy format support goes here and lets just sort of forget it exists.
// Currently its a carbon copy of noshbar's old code and data structures.

namespace TISFAT.Util.Legacy
{
	namespace Functions
	{
		public static class Helpers
		{
			public static Color DWORDtoRGB(int color)
			{
				int r, g, b;

				b = color >> 16;
				g = color >> 8;
				r = color;

				return Color.FromArgb(255, r, g, b);
			}

			public static Color ODDCOLORtoCOLOR(byte[] color)
			{
				byte r, g, b, a;

				b = color[0];
				g = color[1];
				r = color[2];
				a = color[3];

				return Color.FromArgb(a, r, g, b);
			}
        }
	}

	namespace Structures
	{
		public enum ObjectsEnum
		{
			O_RECTANGLE = 1,
			O_OVAL = 2, 
			O_SOUND = 3, 
			O_STICKMAN = 4,
			O_POLY = 5,
			O_SUBTITLE = 6,
			O_TEXT = 7,
			O_BITMAP = 8, 
			O_EXPLODE = 9,
			O_LINE = 10, 
			O_STICKMANBMP = 11,
			O_SPECIALSTICK = 12,
			O_T2STICK = 13,
			O_NOTHING = 128
		}

		public enum EditingEnum
		{
			O_EDITVIDEO = 100,
			E_POINTCHANGE = 1,
			E_COLORCHANGE = 2,
			E_MOVEOBJECT = 3,
			E_T2STICK = 4
		}

		public enum ActionsEnum
		{
			A_JUMPTO = 0, 
			A_LOADNEW = 1,
			A_SHAKE = 2,
			A_OLD = 3,
			A_NOTHING = -1
		}

		public enum MiscEnum
		{
			C_STARTFRAME = 16,
			C_ENDFRAME = 32,
			C_TWEENFRAME = 64,
			C_MAXPART = 200
		}


		// Delphi types
		public class TBrushStyle
		{
			public enum Style
			{
				bsSolid = 0, //used
				bsClear, //used
				bsHorizontal,
				bsVertical,
				bsFDiagonal,
				bsBDiagonal,
				bsCross,
				bsDiagCross
			};

			public Style style;
			public TBrushStyle(BinaryReader reader)
			{
				style = (Style)reader.ReadByte();
			}
		}

		public class TPenStyle
		{
			public enum Style
			{
				psSolid = 0, //used
				psDash,
				psDot,
				psDashDot,
				psDashDotDot,
				psClear, //used
				psInsideFrame,
				psUserStyle,
				psAlternate
			};

			public Style style;
			public TPenStyle(BinaryReader reader)
			{
				style = (Style)reader.ReadByte();
			}
		}

		public class TFontStyles
		{
			public enum Style
			{
				fsBold = 0,
				fsItalic,
				fsUnderline,
				fsStrikeOut
			};

			public Boolean Bold;
			public Boolean Italic;
			public Boolean StrikeOut;
			public Boolean Underline;
			public TFontStyles(BinaryReader reader)
			{
				//TODO: not this simple, it's a set of Style
				byte styles = reader.ReadByte();
				Bold = (styles & (1 << (int)Style.fsBold)) != 0;
				Italic = (styles & (1 << (int)Style.fsItalic)) != 0;
				StrikeOut = (styles & (1 << (int)Style.fsStrikeOut)) != 0;
				Underline = (styles & (1 << (int)Style.fsUnderline)) != 0;
			}
		}

		// Custom types
		public class TPntObj
		{
			public int Top;
			public int Left;

			public TPntObj(BinaryReader reader)
			{
				Left = reader.ReadInt32();
				Top = reader.ReadInt32();
			}
		}

		public class TLineObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_Color;
			public int m_body; // ptr

			public TLineObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TExplodeObj : TObject
		{
			public bool m_bInit;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public List<TPntObj> m_Particles;
			public int m_nMidX, m_nMidY;

			public TExplodeObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TTextObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public Color m_InColor, m_OutColor;
			public TBrushStyle m_styleOuter;
			public string m_strFontName;
			public TFontStyles m_FontStyle;
			public string m_strCaption;
			public int m_body; // ptr

			public TTextObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TSubtitleObj
		{
			public string m_strCaption;
		}

		public class TObject
		{
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;

			public void ReadNew(BinaryReader reader)
			{
				m_angle = reader.ReadSingle();
				m_alpha = reader.ReadByte();
				m_aliased = reader.ReadByte();
			}

			static public Color ReadColour(BinaryReader reader)
			{
				byte[] colour = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
				return Functions.Helpers.ODDCOLORtoCOLOR(colour);
			}
		}

		public class TPolyObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			public TBrushStyle m_styleInner;
			public TPenStyle m_styleOuter;
			public int m_body; // ptr

			public TPolyObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TSoundObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			//TLabel Pnt;
			public string m_strFileName;
		}

		public class TOvalObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			public TBrushStyle m_styleInner;
			public TPenStyle m_styleOuter;
			public int m_body; // ptr

			public TOvalObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TSquareObj : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			public TBrushStyle m_styleInner;
			public TPenStyle m_styleOuter;
			public int m_body; // ptr

			public TSquareObj()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TBitman : TObject
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public string m_strFileName;
			public List<TPntObj> PntList;
			public Bitmap Imarge;
			public bool m_bLoadNew;
			public MemoryStream ms;
			public int m_body; // ptr

			public TBitman()
			{
				PntList = new List<TPntObj>();
			}
		}

		public class TStickMan : TObject
		{
			// TpCanvas m_pCanvas;
			public bool m_bMOving;
			public int m_nX, m_nY;
			public int m_nHeadDiam;
			public List<int> Wid; // capacity of 10
			public List<TPntObj> PntList;
						/*
						1-  Hip
						2-  Left knee
						3-  Left foot
						4-  Right knee
						5-  Right foot
						6-  Neck
						7-  Left elbow
						8-  Left hand
						9-   Right elbow
						10- Right hand
						*/
			public List<int> Lng;  // capacity of 9
						/*
						1- Left thigh
						2- Left calf
						3- Right thigh
						4- Right calf
						5- Body
						6- Left elbow
						7- Left hand
						8- Right elbow
						9- Right hand
						*/
			public Color m_InColor, m_OutColor;

			public TStickMan()
			{
				PntList = new List<TPntObj>();
				Wid = new List<int>();
				Lng = new List<int>();
			}
		}

		public class TSpecialStickMan : TObject
		{
			public int m_nDrawStyle;
			// TpCanvas m_pCanvas;
			public bool m_bMoving;
			public int m_nX, m_nY;
			public int m_nHeadDiam;
			public List<int> Wid; // capacity of 14
			public List<TPntObj> PntList;
			public List<int> Lng; // capacity of 13
			public Color m_InColor, m_OutColor;
			public int m_nLineWidth;
			public TBrushStyle m_styleInner;
			public TPenStyle m_styleOuter;

			public TSpecialStickMan()
			{
				PntList = new List<TPntObj>();
				Wid = new List<int>();
				Lng = new List<int>();
			}
		}

		public class TStickManBMP : TObject
		{
			// TpCanvas m_pCanvas;
			public bool m_bMoving;
			public int m_nX, m_nY;
			public int m_nHeadDiam;
			public bool m_bMouthOpen;
			public bool m_bFlipped;
			public Bitmap m_FaceClosed, m_FaceOpen;
			public List<int> Wid; // capacity of 10
			public List<TPntObj> PntList;
			public List<int> Lng; // capacity of 9
			public Color m_InColor, m_OutColor;
			public MemoryStream ms;

			public TStickManBMP()
			{
				PntList = new List<TPntObj>();
				Wid = new List<int>();
				Lng = new List<int>();
			}
		}

		public class TActionObj
		{
			public int m_nType;
			public int[] m_nParams;
			public string m_strParam;
			public int m_nFrameNo;
		}

		public class TIFrame
		{
			public int m_nType;
			public TObject m_pObject; // ptr
			public int m_FrameNo;
			public int m_nOnion;
		}

		public class TSingleFrame
		{
			public List<TIFrame> m_Frames;
			public int m_Type; // tween or normal draw
		}

		public class TLayerObj
		{
			public TObject m_pObject; // ptr
			public TObject m_pTempObject; // ptr
			public int m_nType;
			public List<TSingleFrame> m_olFrames;
			public string m_strName;
			public List<TActionObj> m_olActions;
			public bool m_bHidden;
		}

		public class TEditVideoObj : TObject
		{
			public string m_strFileName;
			public List<TPntObj> PntList;
		}

		public class TLimbList : TObject
		{
			public List<TJoint> m_olJoints;
			public bool m_bCalcIK;
			public int m_nIndex;
			// public TCanvas m_pCanvas;
			public int m_nJointCount;
			public bool m_bShowJoints;
			public float m_sAlpha;
			public int m_nDrawMode;
			public float m_sTension;
			public int m_nCurveWidth;
			public int m_nPoseCount;
			public int m_nBitmapCount;
			public List<Bitmap> m_olBitmaps;
			public bool bShallowBitmaps;

			public void Read(BinaryReader reader)
			{
				m_olJoints = new List<TJoint>();
				m_olBitmaps = new List<Bitmap>();

				m_nDrawMode = reader.ReadInt32();
				m_sTension = reader.ReadSingle();
				m_nCurveWidth = reader.ReadInt32();
				m_nJointCount = reader.ReadInt32();
				int nCount = reader.ReadInt32();

				for (int i = 0; i < nCount; i++)
				{
					TJoint joint = new TJoint();
					joint.Read(reader);
					m_olJoints.Add(joint);
				}

				m_nPoseCount = reader.ReadInt32();
				m_sAlpha = 1.0f - (m_nPoseCount / 255.0f);

				m_nBitmapCount = reader.ReadInt32();
				for (int f = 0; f < m_nBitmapCount; f++)
				{
					string bitmapName = "";
					int nameLength = reader.ReadInt32();
					reader.ReadByte(); //the first byte of a pascal string is the length
					if (nameLength > 1)
						bitmapName = new string(reader.ReadChars(nameLength - 1));
											
					Bitmap bmp = FileFormat.LoadBitmap(reader);
					m_olBitmaps.Add(bmp);
				}
			}
		}

		public class TJoint : TObject
		{
			public int m_nX, m_nY;
			public int m_nState;
			public int m_nLength;
			public int m_nIndex;
			public TJoint m_pParent;
			public float m_sAngleToParent;
			public int m_nBitmap;
			public bool m_bShowLine;
			public int m_nBMPXoffs, m_nBMPYoofs;
			public float m_sBitmapRotation;
			public byte m_nBitmapAlpha;
			public int m_nLineWidth;
			public Color m_nColor;
			public Color m_nInColor;
			public bool m_bFill;
			public int m_nDrawAs;
			public int m_nDrawWidth;
			public int m_pData; // ptr
			public string m_strName;
			public List<TJoint> m_olChildren;

			public void Read(BinaryReader reader)
			{
				int f;
				TJoint pChild;
				int nCount;

				m_olChildren = new List<TJoint>();

				m_nDrawAs = reader.ReadInt32();
				m_nDrawWidth = reader.ReadInt32();
				m_nLineWidth = reader.ReadInt32();
				m_bShowLine = reader.ReadBoolean();
				byte[] color = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
				m_nColor = Functions.Helpers.ODDCOLORtoCOLOR(color);
				byte[] inColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
				m_nInColor = Functions.Helpers.ODDCOLORtoCOLOR(inColor);
				m_bFill = reader.ReadBoolean();
				m_nX = reader.ReadInt32();
				m_nY = reader.ReadInt32();
				m_nState = reader.ReadInt32();
				m_nLength = reader.ReadInt32();
				m_nIndex = reader.ReadInt32();
				m_sAngleToParent = reader.ReadSingle();
				m_nBitmap = reader.ReadInt32();
				m_nBMPXoffs = reader.ReadInt32();
				m_nBMPYoofs = reader.ReadInt32();
				m_sBitmapRotation = reader.ReadSingle();
				m_nBitmapAlpha = reader.ReadByte();
				nCount = reader.ReadInt32();

				for(f = 0; f < nCount; f++)
				{
					pChild = new TJoint();
					pChild.m_pParent = this;
					pChild.Read(reader);
					m_olChildren.Add(pChild);
				}
			}
		}
	}

	public static class FileFormat
	{
		public static Bitmap LoadBitmap(BinaryReader reader)
		{
			int l = reader.ReadInt32();

			byte[] img = reader.ReadBytes(l);
			MemoryStream ms = new MemoryStream(img);

			Bitmap bitmap = (Bitmap)Image.FromStream(ms);

			ms.Close();
			ms.Dispose();

			return bitmap;
		}

		public static void Load(string FileName)
		{
			int f, g, h, i;
			int nFrameSetCount, nFramesCount;
			int nWide, nHigh;
			int nType;
			string strLayerName;
			bool bRead;
			int nActionCount;
			int misc;
			bool bLoadNew;
			bool bTrans;
			bool bFirstlayer;
			bool bMore;
			int nSkip;
			bool bNewFormat;
			List<TLayerObj> pLayers;

			BinaryReader reader;


			reader = new BinaryReader(new FileStream(FileName, FileMode.Open));

			f = reader.ReadInt32();
			g = reader.ReadInt32();
			h = reader.ReadInt32();
			i = reader.ReadInt32();

			bFirstlayer = true;
			bNewFormat = false;

			if (f != 'I' || g != 'H' || h != '8')
			{
				// This isn't a TISFAT file, or it's corrupted somehow.
				reader.Close();
				reader.Dispose();
				return;
			}

			if (i == 'U')
				bFirstlayer = false;
			else if (i == 'I')
				bFirstlayer = true;
			else if (i == 'V')
			{
				bFirstlayer = true;
				bNewFormat = true;
			}
			else
			{
				// This isn't a TISFAT file that we can read, or it's corrupted.
				reader.Close();
				reader.Dispose();
				return;
			}

			nWide = reader.ReadInt32();
			nHigh = reader.ReadInt32();

			// NewMovie (nWide, nHigh);

			int FPS = reader.ReadInt32();

			byte[] m_bgColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };

			pLayers = new List<TLayerObj>();

			while (reader.BaseStream.Position < reader.BaseStream.Length)
			{
				strLayerName = "";
				i = reader.ReadInt32();
				reader.ReadByte(); //the first byte of a pascal string is the length
				if (i > 1)
					strLayerName = new string(reader.ReadChars(i - 1));

				nType = reader.ReadInt32();

				nFrameSetCount = reader.ReadInt32();

				//if (!bFirstlayer)  If it is the first layer, reference the project's first layer, otherwise create a new one
				TLayerObj pLayer = new TLayerObj();
				pLayer.m_nType = nType;

				pLayer.m_strName = strLayerName;

				bRead = false;
				nActionCount = reader.ReadInt32();

				pLayer.m_olActions = new List<TActionObj>();

				for(g = 0; g < nActionCount; g++)
				{
					TActionObj action = new TActionObj();

					action.m_nType = reader.ReadInt32();
					action.m_nFrameNo = reader.ReadInt32();
					action.m_nParams = new int[3];

					switch(action.m_nType)
					{
						case (int)ActionsEnum.A_JUMPTO:
							action.m_nParams[0] = reader.ReadInt32();
							action.m_nParams[1] = reader.ReadInt32();
							action.m_nParams[2] = reader.ReadInt32();
							break;
						case (int)ActionsEnum.A_SHAKE:
							action.m_nParams[0] = reader.ReadInt32();
							action.m_nParams[1] = reader.ReadInt32();
							action.m_nParams[2] = reader.ReadInt32();
							break;
						case (int)ActionsEnum.A_LOADNEW:
							misc = reader.ReadInt32();
							action.m_strParam = new string(reader.ReadChars(misc));
							break;
						case (int)ActionsEnum.A_OLD:
							action.m_nParams[0] = reader.ReadInt32();
							break;
					}

					pLayer.m_olActions.Add(action);
				}

				pLayer.m_olFrames = new List<TSingleFrame>();

				for (f = 0; f < nFrameSetCount; f++)
				{
					TSingleFrame frameset = new TSingleFrame();
					frameset.m_Type = 6;
					frameset.m_Frames = new List<TIFrame>();

					nFramesCount = reader.ReadInt32();

					for (g = 0; g < nFramesCount; g++)
					{
						TIFrame frame = new TIFrame();
						frame.m_nType = nType;
						if (nType == (int)ObjectsEnum.O_NOTHING)
							frame.m_nType = (int)ObjectsEnum.O_POLY;
						frame.m_nOnion = reader.ReadInt32();
						frame.m_FrameNo = reader.ReadInt32();

						if(pLayer.m_nType == (int)EditingEnum.O_EDITVIDEO)
						{
							TEditVideoObj obj = new TEditVideoObj();

							i = reader.ReadInt32();
							obj.m_strFileName = new string(reader.ReadChars(i));
							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 4; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_RECTANGLE)
						{
							TSquareObj obj = new TSquareObj();
							for (i = 0; i < 4; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							obj.m_nLineWidth = reader.ReadInt32();

							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);

							obj.m_styleInner = new TBrushStyle(reader);
							obj.m_styleOuter = new TPenStyle(reader);

							if (bNewFormat)
								obj.ReadNew(reader);
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_LINE)
						{
							TLineObj obj = new TLineObj();
							for (i = 0; i < 2; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							obj.m_nLineWidth = reader.ReadInt32();
							obj.m_Color = TObject.ReadColour(reader);

							if (bNewFormat)
								obj.ReadNew(reader);
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_EXPLODE)
						{
							TExplodeObj obj = new TExplodeObj();
							for (i = 0; i < 2; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							obj.m_nMidX = reader.ReadInt32();
							obj.m_nMidY = reader.ReadInt32();

							if(g == 0)
							{
								// do something
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_BITMAP)
						{
							bLoadNew = reader.ReadBoolean();
							TBitman obj = new TBitman();

							// the bitmap should only be read on the first keyframe of the layer
							if (!bRead)
							{
								Bitmap bmp;
								if (bLoadNew)
								{
									bmp = LoadBitmap(reader);
								}
								else
								{
									byte[] header = reader.ReadBytes(2); //read the BM
									if (!(header[0] == 'B' && header[1] == 'M'))
									{
										//TODO: if it's not a BMP, abort
									}
									int bmpLength = reader.ReadInt32();  //read the BMP data length
									reader.BaseStream.Seek(-6, SeekOrigin.Current); //go back to the beginning of it
									byte[] bmpData = reader.ReadBytes(bmpLength);   //read the entire BMP file
									MemoryStream bmpStream = new MemoryStream(bmpData);
									bmp = (Bitmap)Image.FromStream(bmpStream);
								}
								bTrans = reader.ReadBoolean();
								bRead = true;
								obj.Imarge = bmp;
							}
							else
							{
								obj.Imarge = ((TBitman)frame.m_pObject).Imarge;
							}

							for (i = 0; i < 4; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_STICKMANBMP)
						{
							TStickManBMP obj = new TStickManBMP();

							if(!bRead)
							{
								obj.m_FaceClosed = LoadBitmap(reader);
								obj.m_FaceOpen = LoadBitmap(reader);
								bRead = true;
							}

							obj.m_nHeadDiam = reader.ReadInt32();

							for(i = 0; i < 10; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							for(i = 0; i < 10; i++)
							{
								obj.Wid.Add(reader.ReadInt32());
							}

							for(i = 0; i < 9; i++)
							{
								obj.Lng.Add(reader.ReadInt32());
							}

							obj.m_OutColor = TObject.ReadColour(reader);
							obj.m_bMouthOpen = reader.ReadBoolean();
							obj.m_bFlipped = reader.ReadBoolean();

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_TEXT)
						{
							TTextObj obj = new TTextObj();
							for (i = 0; i < 4; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);

							obj.m_styleOuter = new TBrushStyle(reader);
							obj.m_FontStyle = new TFontStyles(reader);

							i = reader.ReadInt32();
							reader.ReadByte();
							if (i > 0)
								obj.m_strFontName = new string(reader.ReadChars(i - 1));
							i = reader.ReadInt32();
							reader.ReadByte();
							if (i > 0)
								obj.m_strCaption = new string(reader.ReadChars(i - 1));

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_OVAL)
						{
							TOvalObj obj = new TOvalObj();
							for (i = 0; i < 4; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							obj.m_nLineWidth = reader.ReadInt32();
							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);
							obj.m_styleInner = new TBrushStyle(reader);
							obj.m_styleOuter = new TPenStyle(reader);

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_STICKMAN)
						{
							TStickMan obj = new TStickMan();
							obj.m_nHeadDiam = reader.ReadInt32();
							for (i = 0; i < 10; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							for(i = 0; i < 10; i++)
								obj.Wid.Add(reader.ReadInt32());

							for (i = 0; i < 9; i++)
								obj.Lng.Add(reader.ReadInt32());

							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_T2STICK)
						{
							TLimbList obj = new TLimbList();

							if(!bRead)
							{
								obj.Read(reader);
								//obj.CopyFrom(pLayer.m_pTempObject);
								bRead = true;
							}
							else
							{
								obj.Read(reader);
							}

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_SPECIALSTICK)
						{
							TSpecialStickMan obj = new TSpecialStickMan();

							obj.m_nDrawStyle = reader.ReadInt32();

							// Set pLayer.m_nDrawStyle = m_nDrawStyle
							// Set pLayer.m_pTempObject.m_nDrawstyle = m_nDrawStyle

							obj.m_nLineWidth = reader.ReadInt32();
							obj.m_nHeadDiam = reader.ReadInt32();

							obj.m_styleInner = new TBrushStyle(reader);
							obj.m_styleOuter = new TPenStyle(reader);

							for (i = 0; i < 14; i++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							for (i = 0; i < 14; i++)
								obj.Wid.Add(reader.ReadInt32());

							for (i = 0; i < 13; i++)
								obj.Lng.Add(reader.ReadInt32());

							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);

							bMore = reader.ReadBoolean();
							if(bMore)
							{
								nSkip = reader.ReadInt32();
								reader.ReadBytes(nSkip);
							}

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}
						else if (pLayer.m_nType == (int) ObjectsEnum.O_NOTHING || pLayer.m_nType == (int)ObjectsEnum.O_POLY)
						{
							pLayer.m_nType = (int)ObjectsEnum.O_POLY;
							TPolyObj obj = new TPolyObj();

							i = reader.ReadInt32();
							if (pLayer.m_pObject == null)
							{
								pLayer.m_pObject = new TPolyObj();
								pLayer.m_pTempObject = new TPolyObj();
							}

							TPolyObj theObj = pLayer.m_pObject as TPolyObj;

							for(h = 0; h < i; h++)
							{
								obj.PntList.Add(new TPntObj(reader));
							}

							theObj.m_nLineWidth = reader.ReadInt32();
							obj.m_InColor = TObject.ReadColour(reader);
							obj.m_OutColor = TObject.ReadColour(reader);

							theObj.m_styleInner = new TBrushStyle(reader);
							theObj.m_styleOuter = new TPenStyle(reader);

							if (bNewFormat)
								obj.ReadNew(reader);

							frame.m_pObject = obj;
						}

						frameset.m_Frames.Add(frame);
					}
					
					pLayer.m_olFrames.Add(frameset);
				}

				pLayers.Add(pLayer);
			}
		}
	}
}
