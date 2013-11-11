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
			Layer x = new StickLayer("nopenpeon");

			x.Framesets.Add(new Frameset(new StickFrame(5), 30));
			x.Framesets.Add(new Frameset(new StickFrame(5), 70));

			Console.WriteLine(x.BinarySearch(29));
			Console.ReadKey(true);
		}
	}
}