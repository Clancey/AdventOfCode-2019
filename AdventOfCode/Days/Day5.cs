using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day5 : Day {

		public override void SolvePart1 ()
		{
			//Once you have a working computer, the first step is to restore the gravity assist program (your puzzle input) to the "1202 program alarm"
			//state it had just before the last computer caught fire. To do this, before running the program,
			//replace position 1 with the value 12 and replace position 2 with the value 2.
			//What value is left at position 0 after the program halts?
			var answer = RunProgram (1);
		}
		List<int> input = new List<int> ();
		int RunProgram (int arg)
		{
			input = this.ReadInputString ().Split (",").Select (int.Parse).ToList ();
			//input [1] = arg;
			//input [2] = arg1;
			Console.WriteLine ($"Input: {arg}");
			currentIndex = 0;

			bool shouldContinue = true;
			while (shouldContinue) {
				var fullOpCode = ReadCode ();
				var codeResult = ParseOpCode (fullOpCode.ToString ());
				var opCode = codeResult.code;
				var propCodes = codeResult.paramModes;
				if (opCode == 99) {
					shouldContinue = false;
					break;
				}
				//OpCode3
				if (opCode == 3) {
					var position = ReadCode ();
					input [position] = arg;
				} else if (opCode == 4) {
					var position = ReadCode ();
					var value= ReadParameter (position, 0, propCodes);
					Console.WriteLine($"Output:{value}");

				}
				else if(opCode == 5) {
					var p1 = ReadParameter(ReadCode (),0,propCodes);
					var p2 = ReadParameter (ReadCode (), 1, propCodes);
					if (p1 != 0) {
						currentIndex = p2;
					}

				}
				else if(opCode == 6) {
					var p1 = ReadParameter (ReadCode (), 0, propCodes);
					var p2 = ReadParameter (ReadCode (), 1, propCodes);
					if (p1 == 0) {
						currentIndex = p2;
					}
				}
				else if(opCode == 7) {
					var p1 = ReadParameter (ReadCode (), 0, propCodes);
					var p2 = ReadParameter (ReadCode (), 1, propCodes);
					var p3 = ReadCode ();
					input [p3] = p1 < p2 ? 1 : 0;
				}
				else if(opCode == 8) {
					var p1 = ReadParameter (ReadCode (), 0, propCodes);
					var p2 = ReadParameter (ReadCode (), 1, propCodes);
					var p3 = ReadCode ();
					input [p3] = p1 == p2 ? 1 : 0;
				}
				else if (opCode == 1 || opCode == 2) {
					var xIndex = ReadCode ();
					var yIndex = ReadCode ();
					var outputIndex = ReadCode ();

					var x = ReadParameter (xIndex, 0, propCodes);
					var y = ReadParameter (yIndex, 1, propCodes);

					int answer;
					if (opCode == 1)
						answer = x + y;
					else if (opCode == 2)
						answer = x * y;
					else
						throw new Exception ($"Invalid OpCode: {opCode} - {currentIndex}");
					input [outputIndex] = answer;
				} else
					throw new Exception ($"Invalid OpCode: {opCode}- {currentIndex}");
			}
			return input [0];
		}
		int currentIndex = 0;
		public int ReadCode ()
		{
			var code = input [currentIndex];
			currentIndex++;
			return code;
		}

		public int ReadParameter(int inValue, int index, List<int> modes)
		{
			var mode = modes.Count <= index ? 0 : modes [index];
			if (mode == 0)
				return input [inValue];
			return inValue;
		}

		public (int code, List<int> paramModes) ParseOpCode (string code)
		{
			if (code.Length <= 2)
				return (int.Parse (code), new List<int>());
			var firstLength = code.Length - 2;

			var firstPart = code.Substring (0, firstLength).PadLeft(3,'0');
			var secondPart = code.Substring (firstLength);
			var modes =
			firstPart.Select (x => int.Parse(x.ToString())).ToList ();
			modes.Reverse ();
			return (int.Parse (secondPart), modes);
		}

		public override void SolvePart2 ()
		{
			var answer = RunProgram (5);
		}
	}
}
