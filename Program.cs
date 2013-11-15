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

			w(x.insertNewKeyFrameAt(5));
			w(x);
			w(x.insertNewKeyFrameAt(6));
			w(x);
			w(x.insertNewKeyFrameAt(21));
			w(x);
			w(x.insertNewKeyFrameAt(20));
			w(x);
			w(x.insertNewKeyFrameAt(1));
			w(x);

			Console.ReadKey(true);
		}

		static void w(object x)
		{
			Console.WriteLine(x);
			Console.WriteLine();
		}
	}
}