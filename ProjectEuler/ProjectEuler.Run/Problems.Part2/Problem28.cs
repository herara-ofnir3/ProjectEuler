using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Run.Problems.Part2
{
	/// <summary>
	/// 「螺旋状に並んだ数の対角線」 †
	/// 1から初めて右方向に進み時計回りに数字を増やしていき, 5×5の螺旋が以下のように生成される:
	/// 
	/// 21	22	23	24	25
	/// 20	7	8	9	10
	/// 19	6	1	2	11
	/// 18	5	4	3	12
	/// 17	16	15	14	13
	/// 両対角線上の数字の合計は101であることが確かめられる.
	/// 
	/// 1001×1001の螺旋を同じ方法で生成したとき, 対角線上の数字の和はいくつか?	/// </summary>
	public class Problem28 : Problem
	{
		public override string Run()
		{
			var len = 1001;
			var matrix = new int[len, len];
			foreach (var yi in Enumerable.Range(0, len))
				foreach (var xi in Enumerable.Range(0, len))
					matrix[yi, xi] = 0;

			var x = len - 1;
			var y = 0;
			var dir = Direction.Left;
			var info = DirMap[dir];
			var ns = Enumerable.Range(1, len * len);
			foreach (var n in ns.Reverse())
			{
				matrix[y, x] = n;

				if (Turn(matrix, x + info.X, y + info.Y))
				{
					dir = info.NextDir;
					info = DirMap[dir];
				}

				x += info.X;
				y += info.Y;
			}

			var diagnals = new List<int>();
			foreach (var i in Enumerable.Range(0, len))
			{
				diagnals.Add(matrix[i, i]);
				diagnals.Add(matrix[i, len - i - 1]);
			}


			var answer = diagnals.Sum() - 1;
			return answer.ToString();
		}

		static bool Turn(int[,] matrix, int x, int y)
		{
			var ylen = matrix.GetLength(0);
			var xlen = matrix.GetLength(1);

			return !(
				0 <= y && y < ylen &&
				0 <= x && x < xlen &&
				matrix[y, x] == 0);
		}

		static readonly Dictionary<Direction, DirInfo> DirMap = new Dictionary<Direction, DirInfo>
		{
			{ Direction.Left,  new DirInfo(-1,  0, Direction.Down) },
			{ Direction.Down,  new DirInfo( 0, +1, Direction.Right) },
			{ Direction.Right, new DirInfo(+1,  0, Direction.Top) },
			{ Direction.Top,   new DirInfo( 0, -1, Direction.Left) },
		};

		struct DirInfo
		{
			public DirInfo(int x, int y, Direction nextDir)
			{
				X = x;
				Y = y;
				NextDir = nextDir;
			}
			public int X { get; }
			public int Y { get; }
			public Direction NextDir { get; }
		}

		enum Direction
		{
			Left,
			Down,
			Right,
			Top
		}
	}
}
