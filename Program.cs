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
			Console.ReadKey(true);
			Console.WriteLine(typeof(KeyFrame).FindMembers(MemberTypes.Property, BindingFlags.SetProperty, null, null)[0].Name);
		}
	}
}
