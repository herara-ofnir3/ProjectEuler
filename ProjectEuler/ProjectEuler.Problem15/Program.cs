using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler.Problem15
{
	class Program
	{
		static void Main(string[] args)
		{
			var length = 20;

			/*
			var l = new int[length + 1, length + 1];
			for (var y = 0; y < length + 1; y++)
			{
				for (var x = 0; x < length + 1; x++)
				{
					l[y, x] = 0;
				}
			}

			var ans = f(l, 0, 0);
			*/

			var edges = new List<Edge<Point>>();

			Func<Point, Point, Edge<Point>> findEdge = 
				(s, t) =>
					edges.SingleOrDefault(x =>
						(x.Source == s && x.Target == t));

			Func<Point, Point, bool> hasEdge = 
				(p1, p2) => findEdge(p1, p2) != null;

			for (var y = 0; y <= length; y++)
			{
				for (var x = 0; x <= length; x++)
				{
					var p = new Point(x, y);
					var right = new Point(x + 1, y);
					var bottom = new Point(x, y + 1);

					if (right.X <= length && !hasEdge(p, right))
					{
						edges.Add(new Edge<Point>(p, right));
					}

					if (bottom.Y <= length && !hasEdge(p, bottom))
					{
						edges.Add(new Edge<Point>(p, bottom));
					}
				}
			}

			var start = new Point(0, 0);
			var g = new Graph(edges);

			var map = new Dictionary<Point, long>();
			foreach (var v in g.Vertices())
				map.Add(v, 0);

			var answer = Search(g, map, start);
			Console.WriteLine(answer);
		}

		/*
		static int f(int[,] l, int a, int b)
		{
			if (0 == l[a, b])
			{
				if (a == l.GetLength(0) - 1 || b == l.GetLength(1) - 1)
					l[a, b] = 1;
				else
					l[a, b] = f(l, a + 1, b) + f(l, a, b + 1);
			}
			return l[a, b];
		}*/

		static long Search(Graph g, Dictionary<Point, long> p, Point s)
		{
			var edges = g.OutEdges(s);

			if (p[s] == 0)
			{
				if (edges.Count() == 0)
					p[s] = 1;
				else
					p[s] = edges.Sum(x => Search(g, p, x.Target));
			}

			return p[s];
		}
	}

	public class Graph
	{
		public Graph(IEnumerable<Edge<Point>> edges)
		{
			var vertices = edges.SelectMany(x => new[] { x.Source, x.Target }).Distinct();
			foreach (var v in vertices)
			{
				_vertexEdges.Add(v, new List<Edge<Point>>());
			}

			foreach (var e in edges)
			{
				_vertexEdges[e.Source].Add(e);
			}
		}

		private readonly Dictionary<Point, List<Edge<Point>>> _vertexEdges = new Dictionary<Point, List<Edge<Point>>>();

		public IEnumerable<Point> Vertices()
		{
			return _vertexEdges.Keys;
		}

		public IEnumerable<Edge<Point>> OutEdges(Point vertex)
		{
			if (!_vertexEdges.ContainsKey(vertex))
			{
				return Enumerable.Empty<Edge<Point>>();
			}

			return _vertexEdges[vertex];
		}
	}

	public class Edge<TV>
	{
		public Edge(TV s, TV t)
		{
			Source = s;
			Target = t;
		}

		public TV Source { get; }
		public TV Target { get; }
	}

	public struct Point
	{
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public int X { get; }
		public int Y { get; }

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(obj, null)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Point)) return false;
			var other = (Point)obj;
			return X == other.X && Y == other.Y;
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}

		public override string ToString()
		{
			return $"({X},{Y})";
		}

		public static bool operator ==(Point left, Point right)
		{
			if (ReferenceEquals(left, right)) return true;
			return left.Equals(right);
		}

		public static bool operator !=(Point left, Point right)
		{
			return !(left == right);
		}
	}
}
