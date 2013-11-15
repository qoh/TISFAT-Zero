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
			Layer x = new StickLayer("asdfasdf");
			w (x.ToString());
			x.insertNewKeyFrameAt(5);
			w (x.ToString());
			Console.ReadKey(true);
		}

		static void w(object x)
		{
			Console.WriteLine(x);
		}
	}
}