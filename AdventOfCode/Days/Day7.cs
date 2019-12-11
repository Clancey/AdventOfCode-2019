using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day7 : Day {
		public Day7 ()
		{
		}

		public override void SolvePart1 ()
		{
			var input = this.ReadInputString ();
			var inputs = Enumerable.Range (0, 5).ToArray();
			int maxOutput = 0;
			foreach(var currentInputs in GetPermitation(inputs,0, inputs.Length - 1)) { 
				var computers = GetComputers ();
				var lastOutput = 0;
				for (var i = 0; i < currentInputs.Count; i++){

					lastOutput = computers [i].RunProgram (input, currentInputs [i], lastOutput);
				}
				maxOutput = Math.Max (lastOutput, maxOutput);
			}
			Console.WriteLine ($"Final Answer: {maxOutput}");
		}
		public List<IntCodeComputer> GetComputers () => Enumerable.Range (0, 5).Select (x => new IntCodeComputer ()).ToList ();

		public void SwapTwoNumber (ref int a, ref int b)
		{
			int temp = a;
			a = b;
			b = temp;
		}
		public IEnumerable<List<int>> GetPermitation (int [] list, int k, int m)
		{
			int i;
			if (k == m) {
				List<int> output = new List<int> ();
				for (i = 0; i <= m; i++)
					output.Add (list [i]);
				yield return output;
			} else
				for (i = k; i <= m; i++) {
					SwapTwoNumber (ref list [k], ref list [i]);
					foreach(var perm in GetPermitation (list, k + 1, m))
						yield return perm;
					SwapTwoNumber (ref list [k], ref list [i]);
				}
		}

		public override void SolvePart2 ()
		{
			throw new NotImplementedException ();
		}

		public class IntCodeComputer {
			List<int> input = new List<int> ();
			public int RunProgram (string inputText, int phase, int arg)
			{
				this.input = inputText.Split (",").Select (int.Parse).ToList ();
				Console.WriteLine ($"Phase:{phase} Input:{arg}");
				currentIndex = 0;
				int inputCount = 0;
				var output = 0;

				bool shouldContinue = true;
				while (shouldContinue) {
					var fullOpCode = ReadCode ();
					var codeResult = ParseOpCode (fullOpCode.ToString ());
					var opCode = codeResult.code;
					var propCodes = codeResult.paramModes;
					if (opCode == 99) {
						shouldContinue = false;
						return output;
					}
					//OpCode3
					if (opCode == 3) {
						var position = ReadCode ();
						input [position] = inputCount == 0 ? phase : arg;
						inputCount++;
					} else if (opCode == 4) {
						var position = ReadCode ();
						var value = ReadParameter (position, 0, propCodes);
						output = value;
						Console.WriteLine ($"Output:{value}");

					} else if (opCode == 5) {
						var p1 = ReadParameter (ReadCode (), 0, propCodes);
						var p2 = ReadParameter (ReadCode (), 1, propCodes);
						if (p1 != 0) {
							currentIndex = p2;
						}

					} else if (opCode == 6) {
						var p1 = ReadParameter (ReadCode (), 0, propCodes);
						var p2 = ReadParameter (ReadCode (), 1, propCodes);
						if (p1 == 0) {
							currentIndex = p2;
						}
					} else if (opCode == 7) {
						var p1 = ReadParameter (ReadCode (), 0, propCodes);
						var p2 = ReadParameter (ReadCode (), 1, propCodes);
						var p3 = ReadCode ();
						input [p3] = p1 < p2 ? 1 : 0;
					} else if (opCode == 8) {
						var p1 = ReadParameter (ReadCode (), 0, propCodes);
						var p2 = ReadParameter (ReadCode (), 1, propCodes);
						var p3 = ReadCode ();
						input [p3] = p1 == p2 ? 1 : 0;
					} else if (opCode == 1 || opCode == 2) {
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

			public int ReadParameter (int inValue, int index, List<int> modes)
			{
				var mode = modes.Count <= index ? 0 : modes [index];
				if (mode == 0)
					return input [inValue];
				return inValue;
			}

			public (int code, List<int> paramModes) ParseOpCode (string code)
			{
				if (code.Length <= 2)
					return (int.Parse (code), new List<int> ());
				var firstLength = code.Length - 2;

				var firstPart = code.Substring (0, firstLength).PadLeft (3, '0');
				var secondPart = code.Substring (firstLength);
				var modes =
				firstPart.Select (x => int.Parse (x.ToString ())).ToList ();
				modes.Reverse ();
				return (int.Parse (secondPart), modes);
			}
		}
	}
}
