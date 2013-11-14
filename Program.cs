using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewKeyFrames
{
	class Program
	{
		static void Main(string[] args)
		{
			Frameset x = new Frameset(typeof(StickFrame), 0, 20);
			x.InsertKeyFrameAt(new StickFrame(), 5);
			Console.ReadKey(true);
		}
	}
}