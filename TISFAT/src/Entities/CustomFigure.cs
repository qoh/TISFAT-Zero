using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TISFAT.Entities
{
	public class CustomFigure : StickFigure
	{
		public override Layer CreateDefaultLayer(uint StartTime, uint EndTime, LayerCreationArgs e)
		{
			
			Tuple<StickFigure, State> data = e.ArgObject as Tuple<StickFigure, State>;

			Root = data.Item1.Root;

			Layer CustomLayer = new Layer(this);

			if (!Program.ActiveProject.LayerCount.ContainsKey(typeof(CustomFigure)))
				Program.ActiveProject.LayerCount.Add(typeof(CustomFigure), 0);

			int CustomLayerCount = ++Program.ActiveProject.LayerCount[typeof(CustomFigure)];
			CustomLayer.Name = "Custom Figure " + CustomLayerCount;

			CustomLayer.Framesets.Add(new Frameset());
			CustomLayer.Framesets[0].Keyframes.Add(new Keyframe(StartTime, CreateRefState(), Util.EntityInterpolationMode.Linear));
			CustomLayer.Framesets[0].Keyframes.Add(new Keyframe(EndTime, CreateRefState(), Util.EntityInterpolationMode.Linear));

			return CustomLayer;
		}
	}
}
