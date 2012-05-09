using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectEuler.Problem4
{
	class Program
	{
		/// <summary>
		/// 3桁の数の積で表される回文数のうち最大のものを求める。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			int n = 999;
			var palindromes = new List<int>();

			for (int i = n; 100 <= i; i--)
			{
				for (int j = n; 100 <= j; j--)
				{
					var multiple = i * j;

					// 積を文字列にします。
					var multipleText = multiple.ToString();

					// 積を反転させます。
					var multipleReverseText = new string(multipleText.Reverse().ToArray());

					// 積と反転した積が一致すれば回文数なので、回文数リストに追加します。
					bool isPalindrome = (multipleText == multipleReverseText);

					if (isPalindrome)
					{
						palindromes.Add(multiple);
					}
				}
			}

			var answer = palindromes.Max();
			Console.WriteLine(answer);
			Console.ReadLine();
		}
	}
}
