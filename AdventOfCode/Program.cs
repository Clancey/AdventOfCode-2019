using System;
using System.Collections.Generic;
using System.IO;
using AdventOfCode.Days;

namespace AdventOfCode {
	class Program {
		static void Main (string [] args)
		{

			var puzzles = new List<Day> {
				//new Day1(),
				//new Day2(),
				//new Day3(),
				//new Day4(),
				new Day6(),
				//new Day8(),
			};

			foreach (var day in puzzles) {
				day.Solve ();
			}
			Console.ReadKey ();
		}
	}
}
