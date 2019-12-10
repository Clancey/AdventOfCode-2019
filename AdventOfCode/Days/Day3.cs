using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day3 : Day {
		public Day3 ()
		{
		}


		public override void SolvePart1 ()
		{
			var wireInputs = this.ReadInputLines ();
			var wires = wireInputs.Select (Wire.Parse).ToList ();
			var intersections = wires [0].IntersectionPoints (wires [1]);
			var result = intersections.Min (x => Math.Abs (x.X) + Math.Abs (x.Y));
			Console.WriteLine ($"Closest Point: {result}");
		}

		public override void SolvePart2 ()
		{
			Console.WriteLine ("throw new NotImplementedException ");
		}


		public class Wire {
			Point current = new Point ();
			public List<Line> Lines { get; } = new List<Line> ();
			public static Wire Parse (string input)
			{
				var instructions = input.Split (",");
				var wire = new Wire ();
				foreach (var i in instructions) {
					var result = Line.Parse (i, wire.current);
					wire.Lines.Add (result.line);
					wire.current = result.nextPoint;
				}
				return wire;
			}

			public List<Point> IntersectionPoints (Wire wire)
			{
				var intersections = Lines.SelectMany (x => wire.Lines.Select (y => y.Intercects (x)).Where (x => x.intersects)).Select (x => x.point).Distinct ().ToList ();
				return intersections;
			}
		}

		public enum Orientation {
			Horizontal,
			Vertical
		}
		public class Line {
			public Point Start { get; set; }
			public Point End { get; set; }

			public int MinX () => Math.Min (Start.X, End.X);
			public int MaxX () => Math.Max (Start.X, End.X);
			public int MinY () => Math.Min (Start.Y, End.Y);
			public int MaxY () => Math.Max (Start.Y, End.Y);


			public (bool intersects, Point point) Intercects (Line line)
			{
				var y = YIntercepts (line);
				var x = XIntercepts (line);
				if (!y.between || !x.between || (x.value == 0 && y.value == 0))
					return (false, Point.Empty);
				return (true, new Point (x.value, y.value));

			}

			(bool between, int value) XIntercepts (Line line)
			{
				var r = Between (line.Start.X, line.End.X, Start.X);
				if (r.between)
					return r;
				return Between (Start.X, End.X, line.Start.X);
			}
			(bool between, int value) YIntercepts (Line line)
			{
				var r = Between (line.Start.Y, line.End.Y, Start.Y);
				if (r.between)
					return r;
				return Between (Start.Y, End.Y, line.Start.Y);
			}
			static (bool between, int value) Between (int p1, int p2, int value)
			{
				var start = Math.Min (p1, p2);
				var end = Math.Max (p1, p2);
				return (value >= start && value <= end, value);
			}
			public static (Line line, Point nextPoint) Parse (string input, Point lastPoint)
			{
				var direction = input [0];
				var distance = int.Parse (input.Substring (1));
				var offsetPoint = direction switch
				{
					'R' => new Point (distance, 0),
					'L' => new Point (-distance, 0),
					'U' => new Point (0, distance),
					'D' => new Point (0, -distance),
					_ => new Point ()
				};

				var nextPoint = new Point (lastPoint.X + offsetPoint.X, lastPoint.Y + offsetPoint.Y);

				var points = new [] { lastPoint, nextPoint }.OrderBy (x => x.X).ThenBy (x => x.Y).ToList ();
				var line = new Line {
					Start = points [0],
					End = points [1]
				};
				return (line, nextPoint);
			}
		}
	}
}