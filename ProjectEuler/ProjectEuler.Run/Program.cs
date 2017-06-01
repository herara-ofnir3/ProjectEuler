using ProjectEuler.Run.Problems;
using System;

namespace ProjectEuler.Run
{
	class Program
	{
		static void Main(string[] args)
		{
			var problem = new Problem17();
			Console.WriteLine(problem.Run());
#if DEBUG
			Console.ReadLine();
#endif
		}
	}
}
