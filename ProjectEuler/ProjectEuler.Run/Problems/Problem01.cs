using System.Linq;

namespace ProjectEuler.Run.Problems
{
	public class Problem01 : Problem
	{
		public override string Run()
		{
			int n = 1000;

			var answer = Enumerable
				.Range(1, n - 1)
				.Where(i => i % 3 == 0 || i % 5 == 0)
				.Sum();

			return answer.ToString();
		}
	}
}
