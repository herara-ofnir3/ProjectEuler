using ProjectEuler.Run.Problems;
using System;

namespace ProjectEuler.Run
{
	class Program
	{
		static void Main(string[] args)
		{
			var problem = new Problem15();
			Console.WriteLine(problem.Run());
#if DEBUG
			Console.ReadLine();
#endif
		}
	}
}
