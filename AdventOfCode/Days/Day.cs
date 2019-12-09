using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day {

		public string FilePath => $"Inputs/{this.GetType ().Name}.txt";
		public string ReadInputString () => File.ReadAllText (FilePath);
		public string[] ReadInputLines () => File.ReadAllLines (FilePath);

		public List<int> ReadInputLinesAsInt ()
			=> ReadInputLines ().Select (x => int.Parse (x)).ToList ();

	}
}
