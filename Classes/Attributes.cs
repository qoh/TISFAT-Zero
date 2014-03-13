using System;
using System.Collections.Generic;

namespace TISFAT_Zero
{
	class Attributes
	{
		private Dictionary<string, dynamic> attributes;

		public int attributeCount
		{
			get { return attributes.Count; }
		}

		public dynamic this[string name]
		{
			get
			{
				dynamic o;

				if (attributes.TryGetValue(name, out o))
					return o;
				return null;
			}

			set
			{
				if (!attributes.ContainsKey(name))
					throw new ArgumentException();

				attributes.Remove(name);
				attributes.Add(name, value);
			}
		}

		public Attributes()
		{
			attributes = new Dictionary<string, dynamic>();
		}

		public void addAttribute(dynamic value, string name)
		{
			if(attributes.ContainsKey(name))
				return;

			attributes.Add(name, value);
		}

		public void removeAttribute(string name)
		{
			if (!attributes.ContainsKey(name))
				return;

			attributes.Remove(name);
		}
	}
}