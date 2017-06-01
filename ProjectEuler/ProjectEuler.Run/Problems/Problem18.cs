using System.Collections.Generic;

namespace ProjectEuler.Run.Problems
{
	/// <summary>
	/// 最大経路の和 その1
	/// </summary>
	public class Problem18 : Problem
	{
		public override string Run()
		{
			var define = new int[][]
			{
				new [] { 75 },
				new [] { 95, 64 },
				new [] { 17, 47, 82 },
				new [] { 18, 35, 87, 10 },
				new [] { 20, 04, 82, 47, 65 },
				new [] { 19, 01, 23, 75, 03, 34 },
				new [] { 88, 02, 77, 73, 07, 63, 67 },
				new [] { 99, 65, 04, 28, 06, 16, 70, 92 },
				new [] { 41, 41, 26, 56, 83, 40, 80, 70, 33 },
				new [] { 41, 48, 72, 33, 47, 32, 37, 16, 94, 29 },
				new [] { 53, 71, 44, 65, 25, 43, 91, 52, 97, 51, 14 },
				new [] { 70, 11, 33, 28, 77, 73, 17, 78, 39, 68, 17, 57 },
				new [] { 91, 71, 52, 38, 17, 14, 91, 43, 58, 50, 27, 29, 48 },
				new [] { 63, 66, 04, 68, 89, 53, 67, 30, 73, 16, 69, 87, 40, 31 },
				new [] { 04, 62, 98, 27, 23, 09, 70, 98, 73, 93, 38, 53, 60, 04, 23 },
			};

			var root = CreateNode(define, 0, 0);
			var answer = root.TotalValue();
			
			return answer.ToString();
		}

		static Node CreateNode(int[][] define, int y, int x)
		{
			if (define.Length <= y || define[y].Length <= x)
				return null;

			return new Node(
				define[y][x],
				CreateNode(define, y + 1, x),
				CreateNode(define, y + 1, x + 1));
		}

		private class Node
		{
			public Node(int value) : this(value, null, null) { }

			public Node(int value, Node left, Node right)
			{
				Value = value;
				Left = left;
				Right = right;
			}

			public int Value { get; }
			public Node Left { get; set; }
			public Node Right { get; set; }

			public int TotalValue()
			{
				var l = Left?.TotalValue() ?? 0;
				var r = Right?.TotalValue() ?? 0;

				if (l < r)
					return Value + r;
				else
					return Value + l;
			}

			public override string ToString()
			{
				return $"{Value},L:{Left?.Value},R:{Right?.Value}";
			}
		}
	}
}
