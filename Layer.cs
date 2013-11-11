using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Linq;

namespace NewKeyFrames
{
	abstract class Layer
	{
		public List<Frameset> Framesets;
		public string LayerName;
		public ushort LayerType;
		public int SelectedKeyframe_Loc; //The position of the selected keyframe in it's respective frameset
		public int SelectedKeyframe_Pos; //The position in the timeline of the selected keyframe.
		public int SelectedFrameset_Loc; //The index of the selected frameset in the list of framesets.

		public StickObject LayerFigure, LayerTweenFigure;

		//Dynamic properties that can be used if need be. Generally, use this if it isn't a shared property between layer types.
		public Attributes Properties;

	}
}

