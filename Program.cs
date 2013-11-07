using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NewKeyFrames
{
	class Program
	{
		static void Main(string[] args)
		{
			RectFrame x = new RectFrame(5);
			Console.WriteLine(((RectFrame)(x.createClone())).isFilled);
			Console.ReadKey(true);
		}
	}
}
