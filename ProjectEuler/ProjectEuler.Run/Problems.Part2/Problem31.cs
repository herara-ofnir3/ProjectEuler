namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// Problem 31 「硬貨の和」
	/// イギリスでは硬貨はポンド£とペンスpがあり，一般的に流通している硬貨は以下の8種類である.
	/// 
	/// 1p, 2p, 5p, 10p, 20p, 50p, £1 (100p) and £2 (200p).
	/// 
	/// 以下の方法で£2を作ることが可能である．
	/// 
	/// 1×£1 + 1×50p + 2×20p + 1×5p + 1×2p + 3×1p
	/// 
	/// これらの硬貨を使って£2を作る方法は何通りあるか?
	/// </summary>
	public class Problem31 : Problem
	{
		public override string Run()
		{
			var coins = new int[] { 200, 100, 50, 20, 10, 5, 2, 1 };
			var answer = F(coins, 200, 0);
			return answer.ToString();
		}

		static int F(int[] coins, int desired, int i)
		{
			if (coins[i] == 1)
				return 1;

			if (desired == 0)
				return 1;

			if (desired < 0)
				return 0;

			var patterns = 0;

			for (var n = 0; coins[i] * n <= desired; n++)
			{
				var rem = desired - (coins[i] * n);
				patterns += F(coins, rem, i + 1);
			}

			return patterns;
		}
	}
}
