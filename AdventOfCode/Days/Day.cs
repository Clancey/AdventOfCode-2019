using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days {
	public abstract class Day {

		public string FilePath => $"Inputs/{this.GetType ().Name}.txt";
		public string ReadInputString () => File.ReadAllText (FilePath);
		public string [] ReadInputLines () => File.ReadAllLines (FilePath);

		public List<int> ReadInputLinesAsInt ()
			=> ReadInputLines ().Select (x => int.Parse (x)).ToList ();

		public abstract void SolvePart1 ();

		public abstract void SolvePart2 ();

		public void Solve ()
		{
			var problemName = this.GetType ().Name;
			Console.WriteLine ("************************************");
			Console.WriteLine ("************************************");
			Console.WriteLine ("************************************");
			Console.WriteLine ($"Solving {problemName}");
			Console.WriteLine ("******************");
			Console.WriteLine ("Part 1");
			Console.WriteLine ("******************");
			Console.WriteLine ("");
			SolvePart1 ();
			Console.WriteLine ("");
			Console.WriteLine ("******************");
			Console.WriteLine ("Part 2");
			Console.WriteLine ("******************");
			Console.WriteLine ("");
			SolvePart2 ();
			Console.WriteLine ("");
			Console.WriteLine ("******************");
			Console.WriteLine ("");
			Console.WriteLine ("");
		}
	}
}
