using System;
using System.IO;
using AdventOfCode.Days;

namespace AdventOfCode {
	class Program {
		static void Main (string [] args)
		{
			Console.WriteLine ("Hello World!");
			var path = Directory.GetCurrentDirectory ();
			var day1 = new Day1 ().Solve ();
			var day1Hard = new Day1 ().Solve2 ();
			var dayAnswer = new Day8 ().Solve (25, 6);
			Console.WriteLine ($"The answer is: {dayAnswer}");
			Console.ReadKey ();
		}
	}
}
