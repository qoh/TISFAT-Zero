using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TISFAT.Util.Legacy.Structures;

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



		public class TPntObj
		{
			public int Top;
			public int Left;
		}

		public class TLineObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_Color;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public int m_body; // ptr
		}

		public class TExplodeObj
		{
			public bool m_bInit;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public List<TPntObj> m_Particles;
			public int m_nMidX, m_nMidY;
		}

		public class TTextObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public Color m_InColor, m_OutColor;
			// TBrushStyle m_styleOuter;
			public string m_strFontName;
			// TFontStyles m_FontStyle;
			public string m_strCaption;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public int m_body; // ptr
		}

		public class TSubtitleObj
		{
			public string m_strCaption;
		}

		public class TPolyObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			// TBrushStyle m_styleInner;
			// TPenStyle m_StyleOuter;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public int m_body; // ptr
		}

		public class TSoundObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			// TLabel Pnt;
			public string m_strFileName;
		}

		public class TOvalObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			// TBrushStyle m_styleInner;
			// TPenStyle m_styleOuter;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public int m_body; // ptr
		}

		public class TSquareObj
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public List<TPntObj> PntList;
			public int m_nLineWidth;
			public Color m_InColor, m_OutColor;
			// TBrushStyle m_styleInner;
			// TPenStyle m_styleOuter;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public int m_body; // ptr
		}

		public class TBitman
		{
			public bool m_bMoving;
			public int m_nX, m_nY;
			public string m_strFileName;
			public List<TPntObj> PntList;
			// TGPBitmap Imarge;
			public bool m_bLoadNew;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public MemoryStream ms;
			public int m_body; // ptr
		}

		public class TStickMan
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
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
		}

		public class TSpecialStickMan
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
			// TBrushStyle m_styleInner;
			// TPenStyle m_styleOuter;
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
		}

		public class TStickManBMP
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
			public byte m_alpha;
			public float m_angle;
			public byte m_aliased;
			public MemoryStream ms;
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
			public int m_pObject; // ptr
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
			public int m_pObject; // ptr
			public int m_pTempObject; // ptr
			public int m_nType;
			public List<object> m_olFrames;
			public string m_strName;
			public List<object> m_olActions;
			public bool m_bHidden;
		}

		public class TEditVideoObj
		{
			public string m_strFileName;
			public List<TPntObj> PntList;
		}

		public class TLimbList
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
		}

		public class TJoint
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
		}
	}

	public static class FileFormat
	{
		public static Bitmap LoadBitmap(BinaryReader reader)
		{
			int l = reader.ReadInt32();

			byte[] img = reader.ReadBytes(l);
			MemoryStream ms = new MemoryStream(img);

			Bitmap bitmap = new Bitmap(ms);

			ms.Close();
			ms.Dispose();

			return bitmap;
		}

		public static void Load(string FileName)
		{
			int f, g, h, i;
			int nFrameSetCount, nFramesCount;
			int x, y;
			int nWide, nHigh;
			int nType;
			string strInfo, strLayerName;
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

			FileStream fs;
			MemoryStream ms;

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
				strInfo = "";

				i = reader.ReadInt32();
				strLayerName = new string(reader.ReadChars(i));

				nType = reader.ReadInt32();

				nFrameSetCount = reader.ReadInt32();

				//if (!bFirstlayer)  If it is the first layer, reference the project's first layer, otherwise create a new one
				TLayerObj pLayer = new TLayerObj();
				pLayer.m_nType = nType;

				pLayer.m_strName = strLayerName;

				bRead = false;
				nActionCount = reader.ReadInt32();

				List<TActionObj> Actions = new List<TActionObj>();

				for(g = 0; g < nActionCount - 1; g++)
				{
					TActionObj action = new TActionObj();

					action.m_nType = reader.ReadInt32();
					action.m_nFrameNo = reader.ReadInt32();

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

					Actions.Add(action);
				}

				List<TSingleFrame> Framesets = new List<TSingleFrame>();

				// for (f = 0; f < nFrameSetCount - 1; f++)
				for (f = 0; f < nFrameSetCount; f++)
				{
					TSingleFrame frameset = new TSingleFrame();
					frameset.m_Type = 6;
					frameset.m_Frames = new List<TIFrame>();

					nFramesCount = reader.ReadInt32();

					// for(g = 0; g < nFramesCount - 1; g++)
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
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
								
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_RECTANGLE)
						{
							TSquareObj obj = new TSquareObj();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 4; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.m_nLineWidth = reader.ReadInt32();
							//obj.m_InColor = reader.ReadBytes(sizeof(m_InColor));
							//obj.m_OutColor = reader.ReadBytes(sizeof(m_OutColor));

							//obj.m_styleInner = reader.ReadBytes(sizeof(m_styleInner));
							//obj.m_styleOuter = reader.ReadBytes(sizeof(m_styleOuter));

							if(bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_LINE)
						{
							TLineObj obj = new TLineObj();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 2; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.m_nLineWidth = reader.ReadInt32();
							byte[] color = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };

							if(bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_EXPLODE)
						{
							TExplodeObj obj = new TExplodeObj();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 2; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
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
							bLoadNew = reader.ReadBoolean(); // Unused

							if (!bRead)
							{
								bTrans = reader.ReadBoolean();
								bRead = true;
							}

							TBitman obj = new TBitman();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 4; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							if(bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
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
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.Wid = new List<int>();

							for(i = 0; i < 10; i++)
							{
								obj.Wid.Add(reader.ReadInt32());
							}

							obj.Lng = new List<int>();

							for(i = 0; i < 9; i++)
							{
								obj.Lng.Add(reader.ReadInt32());
							}

							byte[] m_OutColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };

							obj.m_bMouthOpen = reader.ReadBoolean();
							obj.m_bFlipped = reader.ReadBoolean();

							if (bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_TEXT)
						{
							TTextObj obj = new TTextObj();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 4; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							byte[] m_InColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							byte[] m_OutColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };

							// obj.m_styleOuter = reader.ReadBytes(sizeof(obj.m_styleOuter));
							// obj.m_FontStyle = reader.ReadBytes(sizeof(obj.m_FontStyle));

							i = reader.ReadInt32();
							obj.m_strFontName = new string(reader.ReadChars(i));
							i = reader.ReadInt32();
							obj.m_strCaption = new string(reader.ReadChars(i));

							if (bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_OVAL)
						{
							TOvalObj obj = new TOvalObj();

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 4; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.m_nLineWidth = reader.ReadInt32();
							byte[] m_InColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							byte[] m_OutColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							

							if (bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_STICKMAN)
						{
							TStickMan obj = new TStickMan();

							obj.PntList = new List<TPntObj>();

							obj.m_nHeadDiam = reader.ReadInt32();

							for (i = 0; i < 10; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.Wid = new List<int>();

							for(i = 0; i < 10; i++)
								obj.Wid.Add(reader.ReadInt32());

							obj.Lng = new List<int>();

							for (i = 0; i < 9; i++)
								obj.Lng.Add(reader.ReadInt32());

							byte[] m_InColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							byte[] m_OutColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							if (bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_T2STICK)
						{
							TLimbList obj = new TLimbList();

							if(!bRead)
							{
								// Read from stream
								obj.m_nDrawMode = reader.ReadInt32();
								obj.m_sTension = reader.ReadSingle();
								obj.m_nCurveWidth = reader.ReadInt32();
								obj.m_nJointCount = reader.ReadInt32();
								int nCount = reader.ReadInt32();

								for(i = 0; i < nCount; i++)
								{

								}

								// Copy from temp object
							}
						}
						else if (pLayer.m_nType == (int)ObjectsEnum.O_SPECIALSTICK)
						{
							TSpecialStickMan obj = new TSpecialStickMan();

							obj.m_nDrawStyle = reader.ReadInt32();

							// Set pLayer.m_nDrawStyle = m_nDrawStyle
							// Set pLayer.m_pTempObject.m_nDrawstyle = m_nDrawStyle

							obj.m_nLineWidth = reader.ReadInt32();
							obj.m_nHeadDiam = reader.ReadInt32();

							// obj.m_styleInner = reader.ReadBytes(sizeof(obj.m_styleInner));
							// obj.m_styleOuter = reader.ReadBytes(sizeof(obj.m_styleOuter));

							obj.PntList = new List<TPntObj>();

							for (i = 0; i < 14; i++)
							{
								x = reader.ReadInt32();
								y = reader.ReadInt32();

								TPntObj pnt = new TPntObj();
								pnt.Left = x;
								pnt.Top = y;

								obj.PntList.Add(pnt);
							}

							obj.Wid = new List<int>();

							for (i = 0; i < 14; i++)
								obj.Wid.Add(reader.ReadInt32());

							obj.Lng = new List<int>();

							for (i = 0; i < 13; i++)
								obj.Lng.Add(reader.ReadInt32());

							byte[] m_InColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
							byte[] m_OutColor = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };

							bMore = reader.ReadBoolean();
							if(bMore)
							{
								nSkip = reader.ReadInt32();
								reader.ReadBytes(nSkip);
							}

							if (bNewFormat)
							{
								obj.m_angle = reader.ReadSingle();
								obj.m_alpha = reader.ReadByte();
								obj.m_aliased = reader.ReadByte();
							}
						}
						else if (pLayer.m_nType == (int) ObjectsEnum.O_NOTHING || pLayer.m_nType == (int)ObjectsEnum.O_POLY)
						{
							// stuff
						}

						frameset.m_Frames.Add(frame);
					}

					Framesets.Add(frameset);
				}

				pLayers.Add(pLayer);
			}
		}
	}
}
