using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 日曜日の数え上げ
	/// 
	/// 次の情報が与えられている.
	///
	/// 1900年1月1日は月曜日である.
	/// 9月, 4月, 6月, 11月は30日まであり, 2月を除く他の月は31日まである.
	/// 2月は28日まであるが, うるう年のときは29日である.
	/// うるう年は西暦が4で割り切れる年に起こる. しかし, 西暦が400で割り切れず100で割り切れる年はうるう年でない.
	/// 
	/// 20世紀（1901年1月1日から2000年12月31日）中に月の初めが日曜日になるのは何回あるか?
	/// </summary>
	public class Problem19 : Problem
	{
		public override string Run()
		{
			var start = new DateTime(1901, 1, 1);
			var end = new DateTime(2000, 12, 31);

			var i = 0;
			var d = start;
			while (d <= end)
			{
				if (d.DayOfWeek == DayOfWeek.Sunday)
					i++;

				d = d.AddMonths(1);
			}

			return i.ToString();
		}
	}

	public class Problem19_Another : Problem
	{
		public override string Run()
		{
			var start = new DateTime(1901, 1, 1);
			var end = new DateTime(2000, 12, 31);

			var week = start.DayOfWeek;
			var sundays = 0;

			for (var y = start.Year; y <= end.Year; y++)
			{
				for (var m = 1; m <= 12; m++)
				{
					for (var d = 1; d <= DaysInMonth(y, m); d++)
					{
						if (d == 1 && week == DayOfWeek.Sunday)
							sundays++;

						if (week == DayOfWeek.Saturday)
							week = DayOfWeek.Sunday;
						else
							week++;
					}
				}
			}

			return sundays.ToString();
		}

		static int DaysInMonth(int year, int month)
		{
			if (month == 2)
				return IsLeapYear(year) ? 29 : 28;

			if (days30.Contains(month))
				return 30;

			return 31;
		}

		static bool IsLeapYear(int year)
		{
			return
				year % 4 == 0 &&
				!(year % 400 != 0 && year % 100 == 0);
		}

		static readonly int[] days30 = new int[] { 4, 6, 9, 11 };
	}
}
