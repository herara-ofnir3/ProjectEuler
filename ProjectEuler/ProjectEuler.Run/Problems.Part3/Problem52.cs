using ProjectEuler.Core;
using System;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 52 「置換倍数」
	/// 125874を2倍すると251748となる. これは元の数125874と順番は違うが同じ数を含む.
	/// 
	/// 2x, 3x, 4x, 5x, 6x が x と同じ数を含むような最小の正整数 x を求めよ.
	/// </summary>
	public class Problem52 : Problem
	{
		public override string Run()
		{
			var multi = new int[] { 1, 2, 3, 4, 5, 6 };

			for (var i = 0; ; i++)
			{
				var s = (int)Math.Pow(10, i);
				var e = Math.Pow(10, i + 1) / 6;
				for (var x = s; i <= e; x++)
				{
					var xd = multi
						.Select(m => (x * m).Digits().OrderBy(n => n));

					var match = xd
						.Select((d, idx) => new { d, idx })
						.Skip(1)
						.All(n => n.d.SequenceEqual(xd.ElementAt(n.idx - 1)));

					if (match)
					{
						//var c = multi.Select(m => (x * m)).ToList();
						return x.ToString();
					}
				}
			}
		}
	}
}
