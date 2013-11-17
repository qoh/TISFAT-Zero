using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TISFAT_Zero
{
	class Attributes
	{
		private List<dynamic> attributes;
		private List<string> attributeNames;

		public int attributeCount
		{
			get { return attributes.Count; }
		}

		public dynamic this[string name]
		{
			get { return attributes[attributeNames.BinarySearch(name)]; }

			set { attributes[attributeNames.BinarySearch(name)] = value; }
		}

		public Attributes()
		{
			attributes = new List<dynamic>();
			attributeNames = new List<string>();
		}

		public void addAttribute(dynamic value, string name)
		{
			if (attributeCount == 0)
			{
				attributes.Add(value);
				attributeNames.Add(name);
				return;
			}

			int bottom = 0;
			int top = attributeCount;
			int middle = top >> 1;

			//I had to make this binary search algorithm custom because I need it to store the middle index if the target is not found.
			while (top >= bottom)
			{
				int x = attributeNames[middle].CompareTo(name);

				if (x > 0)
					top = middle - 1;
				else if (x < 0)
					bottom = middle + 1;
				else
					throw new ArgumentException("Attribute already exists");

				middle = (bottom + top) >> 1;
			}

			attributes.Insert(middle, value);
			attributeNames.Insert(middle, name);
		}

		public void removeAttribute(string name)
		{
			int index = attributeNames.BinarySearch(name);

			if (index == -1)
				throw new ArgumentException("Attribute with specified name not found.");

			attributes.RemoveAt(index);
			attributeNames.RemoveAt(index);
		}

		public void removeAttribute(dynamic value)
		{
			int index = attributes.IndexOf(value);

			if (index == -1)
				throw new ArgumentException("Attribute with specified value not found.");

			attributes.RemoveAt(index);
			attributeNames.RemoveAt(index);
		}
	}
}