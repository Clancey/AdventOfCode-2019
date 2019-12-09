using System;
using System.IO;
using AdventOfCode.Days;

namespace AdventOfCode {
	class Program {
		static void Main (string [] args)
		{
			Console.WriteLine ("Hello World!");
			var path = Directory.GetCurrentDirectory ();
			var dayAnswer = new Day8 ().Solve ("Inputs/Day8.txt", 25, 6);
			Console.WriteLine ($"The answer is: {dayAnswer}");
			Console.ReadKey ();
		}
	}
}
