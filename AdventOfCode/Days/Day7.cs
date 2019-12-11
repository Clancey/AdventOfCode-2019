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
			var inputs = Enumerable.Range (0, 5).ToArray ();
			int maxOutput = 0;
			foreach (var currentInputs in GetPermitation (inputs, 0, inputs.Length - 1)) {
				var computers = GetComputers ();
				var lastOutput = 0;
				for (var i = 0; i < currentInputs.Count; i++) {

					lastOutput = computers [i].RunProgram (input, currentInputs [i], lastOutput);
				}
				maxOutput = Math.Max (lastOutput, maxOutput);
			}
			Console.WriteLine ($"Final Answer: {maxOutput}");
		}



		public override void SolvePart2 ()
		{
			var input = this.ReadInputString ();
			var inputs = Enumerable.Range (5, 5).ToArray ();
			int maxOutput = 0;
			foreach (var currentInputs in GetPermitation (inputs, 0, inputs.Length - 1)) {
				var computers = GetComputers ();
				var lastOutput = 0;
				var hasHalted = false;
				while (!hasHalted) {
					for (var i = 0; i < currentInputs.Count; i++) {
						var cpu = computers [i];
						lastOutput = cpu.RunProgram (input, currentInputs [i], lastOutput);
						hasHalted = cpu.Halted;
					}
				}
				maxOutput = Math.Max (lastOutput, maxOutput);
			}
			Console.WriteLine ($"Final Answer: {maxOutput}");

		}

		public List<IntCodeComputer> GetComputers () => Enumerable.Range (0, 5).Select (x => new IntCodeComputer ()).ToList ();

		public void SwapTwo<T> (ref T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}
		public IEnumerable<List<T>> GetPermitation<T> (T [] list, int k, int m)
		{
			int i;
			if (k == m) {
				List<T> output = new List<T> ();
				for (i = 0; i <= m; i++)
					output.Add (list [i]);
				yield return output;
			} else
				for (i = k; i <= m; i++) {
					SwapTwo (ref list [k], ref list [i]);
					foreach (var perm in GetPermitation (list, k + 1, m))
						yield return perm;
					SwapTwo (ref list [k], ref list [i]);
				}
		}

		public class IntCodeComputer {
			public bool Halted { get; private set; }
			List<int> input;
			int inputCount = 0;
			int output = 0;
			public int ResetInput () => inputCount = 0;
			public int RunProgram (string inputText, int phase, int arg)
			{
				if ((input?.Count ?? 0) == 0)
					this.input = inputText.Split (",").Select (int.Parse).ToList ();
				//Console.WriteLine ($"Phase:{phase} Input:{arg}");

				bool shouldContinue = true;
				while (shouldContinue) {
					var fullOpCode = ReadCode ();
					var codeResult = ParseOpCode (fullOpCode.ToString ());
					var opCode = codeResult.code;
					var propCodes = codeResult.paramModes;
					if (opCode == 99) {
						Halted = true;
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
						//Console.WriteLine ($"Output:{value}");
						return output;

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
