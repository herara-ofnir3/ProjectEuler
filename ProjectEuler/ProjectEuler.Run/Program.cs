using ProjectEuler.Run.Problems;
using ProjectEuler.Run.Problems.Part2;
using System;
using System.Diagnostics;

namespace ProjectEuler.Run
{
	class Program
	{
		static void Main(string[] args)
		{
			var problem = new Problem41();

			var sw = Stopwatch.StartNew();
			var answer = problem.Run();
			sw.Stop();

			Console.WriteLine("{0} ({1:#,0}ms)", answer, sw.ElapsedMilliseconds);
#if DEBUG
			Console.ReadLine();
#endif
		}
	}
}
