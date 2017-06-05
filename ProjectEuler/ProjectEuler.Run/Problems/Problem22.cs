using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	public class Problem22 : Problem
	{
		public override string Run()
		{
			var raw = string.Empty;
			using (var fs = new FileStream("Assets\\Problem22\\names.txt", FileMode.Open))
			{
				using (var reader = new StreamReader(fs))
				{
					raw = reader.ReadToEnd();
				}
			}

			var names = raw
				.Split(',')
				.Select(n => n.Trim('"'))
				.OrderBy(n => n)
				.ToList();

			var answer = names
				.Select((n, i) => (i + 1) * n.Select(c => CharScore[c]).Sum())
				.Sum();

			return answer.ToString();
		}

		private readonly static Dictionary<char, int> CharScore = CreateCharScore();

		static Dictionary<char, int> CreateCharScore()
		{
			var d = new Dictionary<char, int>();

			var source = Enumerable.Range(65, 26)
				.Select((n, i) => new { c = (char)n, score = i + 1 });

			foreach (var s in source)
				d.Add(s.c, s.score);

			return d;
		}
	}
}
