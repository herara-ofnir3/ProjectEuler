using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 辞書式順列
	/// 
	/// 順列とはモノの順番付きの並びのことである. たとえば, 3124は数 1, 2, 3, 4 の一つの順列である. すべての順列を数の大小でまたは辞書式に並べたものを辞書順と呼ぶ. 0と1と2の順列を辞書順に並べると
	/// 
	/// 012 021 102 120 201 210
	/// になる.
	/// 
	/// 0,1,2,3,4,5,6,7,8,9からなる順列を辞書式に並べたときの100万番目はいくつか?
	/// </summary>
	public class Problem24 : Problem
	{
		public override string Run()
		{
			var n = 1000000;
			var elems = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
			//var elems = new int[] { 0, 1, 2 };

			var answer = Perm(elems)
				.Skip(n - 1)
				.First();

			return string.Join("", answer);
		}

		static IEnumerable<IEnumerable<int>> Perm(int[] items)
		{
			var perm = Enumerable.Empty<IEnumerable<int>>();
			foreach (var i in items)
			{
				perm = Perm(perm, items);
			}
			return perm;
		}

		static IEnumerable<IEnumerable<int>> Perm(IEnumerable<IEnumerable<int>> perm, IEnumerable<int> items)
		{
			if (!perm.Any())
			{
				foreach (var i in items)
					yield return FromSingle(i);

				yield break;
			}

			foreach (var p in perm)
				foreach (var i in items.Except(p))
					yield return Add(p, i);
		}

		/*
		static IEnumerable<IEnumerable<int>> Perm(IEnumerable<IEnumerable<int>> perm, IEnumerable<int> items)
		{
			if (!perm.Any())
			{
				foreach (var i in items)
					yield return FromSingle(i);

				yield break;
			}

			foreach (var p in perm)
				foreach (var i in items)
					yield return Add(p, i);
		}

		static IEnumerable<IEnumerable<int>> Perm(params IEnumerable<int>[] list)
		{
			var perm = Enumerable.Empty<IEnumerable<int>>();
			foreach (var l in list)
			{
				perm = Perm(perm, l);
			}
			return perm;
		}
*/

		static IEnumerable<int> Add(IEnumerable<int> items, int value)
		{
			foreach (var i in items)
				yield return i;

			yield return value;
		}

		static IEnumerable<int> FromSingle(int value)
		{
			yield return value;
		}
	}
}
