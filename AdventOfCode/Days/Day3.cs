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
			var wires = GetWires ();
			var intersections = wires [0].IntersectionPoints (wires [1]);
			var result = intersections.Min (x => Math.Abs (x.X) + Math.Abs (x.Y));
			Console.WriteLine ($"Closest Point: {result}");
		}

		public override void SolvePart2 ()
		{
			var wires = GetWires ();
			var intersections = wires [0].IntersectingLines (wires [1]);
			var result = intersections.Min (x => x.line.Length(x.point) + x.line2.Length(x.point));
			Console.WriteLine ($"Closest Length: {result}");
		}

		public List<Wire> GetWires()
		{

			var wireInputs = this.ReadInputLines ();
			var wires = wireInputs.Select (Wire.Parse).ToList ();
			return wires;
		}

		public class Wire {
			Point current = new Point ();
			public List<Line> Lines { get; } = new List<Line> ();
			public static Wire Parse (string input)
			{
				var instructions = input.Split (",");
				var wire = new Wire ();
				Line previousLine = null;
				foreach (var i in instructions) {
					var result = Line.Parse (i, wire.current);
					result.line.Previous = previousLine;
					previousLine = result.line;
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
			public List<(Line line, Line line2, Point point)> IntersectingLines (Wire wire)
			{
				var intersections = Lines.SelectMany (x => wire.Lines.Select (y => y.Intercects (x))).Where (x => x.intersects)
					.Select (x => (x.line,x.line2, x.point)).ToList();
				return intersections;
			}
		}
		public class Line {
			public Point Start { get; set; }
			public Point End { get; set; }
			public Line Previous { get; set; }


			public int Length (Point? interSectionPoint = null)
			{
				var end = interSectionPoint ?? End;
				var length = Math.Abs (Start.X - end.X) + Math.Abs (Start.Y - end.Y);
				var previousLength = (this.Previous?.Length() ?? 0);
				return length + previousLength;
			}


			public (bool intersects, Point point, Line line, Line line2) Intercects (Line line)
			{
				var y = YIntercepts (line);
				var x = XIntercepts (line);
				if (!y.between || !x.between || (x.value == 0 && y.value == 0))
					return (false, Point.Empty, line, this);
				return (true, new Point (x.value, y.value), line,this);

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

				var line = new Line {
					Start = lastPoint,
					End = nextPoint
				};
				return (line, nextPoint);
			}
		}
	}
}