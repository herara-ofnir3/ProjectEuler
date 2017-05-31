using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Problem14
{
	class Program
	{
		static void Main(string[] args)
		{
			var longest = new List<long>();

			for (var i = 1; i < 1000000; i++)
			{
				if (longest.Contains(i))
					continue;

				var numbers = CollatzSeq(i).ToList();
				
				if (longest.Count < numbers.Count)
				{
					longest = numbers;
				}
			}

			var answer = longest.First();
			Console.WriteLine(answer);
		}

		static IEnumerable<long> CollatzSeq(long start)
		{
			var n = start;
			yield return n;

			while (n != 1)
			{
				if (n % 2 == 0)
				{
					n /= 2;
				}
				else
				{
					n = 3 * n + 1;
				}

				yield return n;
			} 		
		}
	}
}
