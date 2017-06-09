using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Core
{
	public static class Prime
	{
		public static IEnumerable<int> Gen(int max)
		{
			var map = new Dictionary<int, bool>();
			foreach (var l in Enumerable.Range(2, max - 1))
				map.Add(l, true);

			var head = 0;
			var sqrt = Math.Sqrt(max);

			while (head <= sqrt)
			{
				head = map
					.Where(x => x.Value)
					.First(x => head < x.Key)
					.Key;

				yield return head;

				var removes = Multiples(head)
					.Skip(1)
					.TakeWhile(x => x <= max);

				foreach (var r in removes)
					map[r] = false;
			}

			foreach (var m in map.Where(m => head < m.Key && m.Value))
				yield return m.Key;
		}

		static IEnumerable<int> Multiples(int n)
		{
			for (var m = n; ; m += n)
			{
				yield return m;
			}
		}
	}
}
