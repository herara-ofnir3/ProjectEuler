using ProjectEuler.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 数字の文字数
	/// 
	/// 1 から 5 までの数字を英単語で書けば one, two, three, four, five であり, 全部で 3 + 3 + 5 + 4 + 4 = 19 の文字が使われている.
	///
	/// では 1 から 1000 (one thousand) までの数字をすべて英単語で書けば, 全部で何文字になるか.
	///
	/// 注: 空白文字やハイフンを数えないこと. 例えば, 342 (three hundred and forty-two) は 23 文字, 115 (one hundred and fifteen) は20文字と数える. なお, "and" を使用するのは英国の慣習.
	/// </summary>
	public class Problem17 : Problem
	{
		public override string Run()
		{
			var answer = Enumerable.Range(1, 1000)
				.SelectMany(n => ToWord(n))
				.Count(c => c != ' ' && c != '-');

			//foreach (var n in Enumerable.Range(1, 300))
			//{
			//	var word = ToWord(n);
			//	Console.WriteLine($"{n}: {word}");
			//}

			return answer.ToString();
		}

		static string ToWord(int n)
		{
			if (n == 0)
				return string.Empty;

			if (n < 1 || 1000 < n)
				throw new ArgumentOutOfRangeException("n");

			if (Map.ContainsKey(n))
				return Map[n];

			var digits = n.Digits().ToList();

			if (digits.Count == 2)
			{
				return Map[digits[1] * 10] + "-" + ToWord(digits[0]);
			}

			var s = new[]
			{
				ToWord(digits[2]) + " hundred",
				ToWord(digits[1] * 10 + digits[0]),
			};

			return string.Join(" and ", s.Where(x => !string.IsNullOrEmpty(x)));
		}

		static readonly Dictionary<int, string> Map = new Dictionary<int, string>
		{
			{ 1, "one" },
			{ 2, "two" },
			{ 3, "three" },
			{ 4, "four" },
			{ 5, "five" },
			{ 6, "six" },
			{ 7, "seven" },
			{ 8, "eight" },
			{ 9, "nine" },
			{ 10, "ten" },
			{ 11, "eleven" },
			{ 12, "twelve" },
			{ 13, "thirteen" },
			{ 14, "fourteen" },
			{ 15, "fifteen" },
			{ 16, "sixteen" },
			{ 17, "seventeen" },
			{ 18, "eighteen" },
			{ 19, "nineteen" },
			{ 20, "twenty" },
			{ 30, "thirty" },
			{ 40, "forty" },
			{ 50, "fifty" },
			{ 60, "sixty" },
			{ 70, "seventy" },
			{ 80, "eighty" },
			{ 90, "ninety" },
			{ 1000, "one thousand" }
		};
	}
}
