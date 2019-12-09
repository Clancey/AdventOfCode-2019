using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day2 : Day {
		public Day2 ()
		{
		}

		public override void SolvePart1 ()
		{
			//Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm"
			//state it had just before the last computer caught fire. To do this, before running the program,
			//replace position 1 with the value 12 and replace position 2 with the value 2.
			//What value is left at position 0 after the program halts?
			var input = this.ReadInputString ().Split (",").Select (int.Parse).ToList ();
			input [1] = 12;
			input [2] = 2;
			int i = 0;

			bool shouldContinue = true;
			while(shouldContinue) {
				var opIndex = i * 4;
				var opCode = input [opIndex];
				if(opCode == 99) {
					shouldContinue = false;
					break;
				}
				var xIndex = input [opIndex + 1];
				var yIndex = input [opIndex + 2];
				var outputIndex = input [opIndex + 3];

				var x = input [xIndex];
				var y = input [yIndex];

				int answer;
				if (opCode == 1)
					answer = x + y;
				else if (opCode == 2)
					answer = x * y;
				else
					throw new Exception ($"Invalid OpCode: {opCode}");
				if(outputIndex == 0) {

				}
				input [outputIndex] = answer;
				i++;

			}

			var finalAnswer = input [0];
			Console.WriteLine ($"Output: {finalAnswer}");
		}

		

		public override void SolvePart2 ()
		{
			Console.WriteLine ("throw new NotImplementedException ()");
		}
	}
}
