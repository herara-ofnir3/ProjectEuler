using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectEuler.Run.Problems.Part3
{
	/// <summary>
	/// Problem 54 「ポーカーハンド」 †
	/// カードゲームのポーカーでは, 手札は5枚のカードからなりランク付けされている. 役を低い方から高い方へ順に並べると以下である.
	/// 
	/// 役無し(ハイカード): 一番値が大きいカード
	/// ワン・ペア: 同じ値のカードが2枚
	/// ツー・ペア: 2つの異なる値のペア
	/// スリーカード: 同じ値のカードが3枚
	/// ストレート: 5枚の連続する値のカード
	/// フラッシュ: 全てのカードが同じスート (注: スートとはダイヤ・ハート・クラブ/スペードというカードの絵柄のこと)
	/// フルハウス: スリーカードとペア
	/// フォーカード: 同じ値のカードが4枚
	/// ストレートフラッシュ: ストレートかつフラッシュ
	/// ロイヤルフラッシュ: 同じスートの10, J, Q, K, A
	/// ここでカードの値は小さい方から2, 3, 4, 5, 6, 7, 8, 9, 10, J, Q, K, Aである. (訳注：データ中で10は'T'と表される)
	/// 
	/// もし2人のプレイヤーが同じ役の場合には, 役を構成する中で値が最も大きいカードによってランクが決まる: 例えば, 8のペアは5のペアより強い (下の例1を見よ). それでも同じランクの場合には (例えば, 両者ともQのペアの場合), 一番値が大きいカードによってランクが決まる (下の例4を見よ). 一番値が大きいカードが同じ場合には, 次に値が大きいカードが比べれられ, 以下同様にランクを決定する.
	/// 
	/// 例:
	/// 
	/// 試合	プレイヤー1	プレイヤー2	勝者
	/// 1	5H 5C 6S 7S KD
	/// 5のペア	2C 3S 8S 8D TD
	/// 8のペア	プレイヤー2
	/// 2	5D 8C 9S JS AC
	/// 役無し, A	2C 5C 7D 8S QH
	/// 役無し, Q	プレイヤー1
	/// 3	2D 9C AS AH AC
	/// Aのスリーカード	3D 6D 7D TD QD
	/// ダイヤのフラッシュ	プレイヤー2
	/// 4	4D 6S 9H QH QC
	/// Qのペア, 9	3D 6D 7H QD QS
	/// Qのペア, 7	プレイヤー1
	/// 5	2H 2D 4C 4D 4S
	/// 4-2のフルハウス	3C 3D 3S 9S 9D
	/// 3-9のフルハウス	プレイヤー1
	/// poker.txtには1000個のランダムな手札の組が含まれている. 各行は10枚のカードからなる (スペースで区切られている): 最初の5枚がプレイヤー1の手札であり, 残りの5枚がプレイヤー2の手札である. 以下のことを仮定してよい
	/// 
	/// 全ての手札は正しい (使われない文字が出現しない. 同じカードは繰り返されない)
	/// 各プレイヤーの手札は特に決まった順に並んでいるわけではない
	/// 各勝負で勝敗は必ず決まる
	/// 1000回中プレイヤー1が勝つのは何回か? (訳注 : この問題に置いてA 2 3 4 5というストレートは考えなくてもよい)	/// </summary>
	public class Problem54 : Problem
	{
		public override string Run()
		{
			var games = new List<Game>();
			using (var fs = new FileStream("Assets\\Problem54\\poker.txt", FileMode.Open))
			{
				using (var reader = new StreamReader(fs))
				{
					while (-1 < reader.Peek())
						games.Add(Game.Parse(reader.ReadLine()));
				}
			}

			var answer = games.Count(g => g.Winner() == 1);
			return answer.ToString();
		}

		class Game
		{
			Game(Hand p1, Hand p2)
			{
				_p1 = p1; _p2 = p2;
			}

			Hand _p1;
			Hand _p2;

			public static Game Parse(string s)
			{
				var cards = s.Split(' ').Select(x => ParseCard(x));
				return new Game(
					new Hand(cards.Take(5)), 
					new Hand(cards.Skip(5)));
			}

			public int Winner()
			{
				if (0 < _p1.Rank().CompareTo(_p2.Rank()))
					return 1;
				else
					return 2;
			}
		}

		class Hand
		{
			public Hand(IEnumerable<(int number, Suit suit)> cards)
			{
				_cards = cards.OrderBy(c => c.number).ToArray();
			}

			readonly (int number, Suit suit)[] _cards;

			public Rank Rank()
			{
				if (IsRoyalFlush())
					return new Rank(RankType.RoyalFlush, 0);

				if (IsFlush() && IsStraight())
					return new Rank(RankType.StraightFlush, _cards.Max(c => c.number));

				return new Rank();
			}

			bool IsRoyalFlush() => IsFlush() && IsStraight() && _cards[0].number == 10;
			bool IsFlush() => _cards.GroupBy(c => c.suit).Count() == 0;
			bool IsStraight() => _cards.Select((c, i) => new { c, i }).Skip(1).All(x => x.c.number == _cards[x.i - 1].number + 1);
			
		}

		enum Suit
		{
			Spade,
			Club,
			Diamond,
			Heart,
		}

		struct Rank : IComparable<Rank>
		{
			public Rank(RankType type, int weight)
			{
				Type = type; Weight = weight;
			}

			public RankType Type { get; }
			public int Weight { get; }

			public int CompareTo(Rank other)
			{
				var typeCompared = Type.CompareTo(other.Type);
				if (typeCompared != 0)
					return typeCompared;

				return Weight.CompareTo(other.Weight);
			}
		}

		enum RankType
		{
			HighCard,
			OnePair,
			TwoPair,
			ThreeOfAKind,
			Straight,
			Flush,
			FullHouse,
			FourOfAKind,
			StraightFlush,
			RoyalFlush,
		}

		static (int number, Suit suit) ParseCard(string s)
		{
			return (number: numberMap[s[0]], suit: suitMap[s[1]]);
		}

		static Dictionary<char, int> numberMap = new Dictionary<char, int>
		{
			{ '2', 2 },
			{ '3', 3 },
			{ '4', 4 },
			{ '5', 5 },
			{ '6', 6 },
			{ '7', 7 },
			{ '8', 8 },
			{ '9', 9 },
			{ 'T', 10 },
			{ 'J', 11 },
			{ 'Q', 12 },
			{ 'K', 13 },
			{ 'A', 14 },
		};

		static Dictionary<char, Suit> suitMap = new Dictionary<char, Suit>
		{
			{ 'S', Suit.Spade },
			{ 'C', Suit.Club },
			{ 'D', Suit.Diamond },
			{ 'H', Suit.Heart },
		};

	}
}
