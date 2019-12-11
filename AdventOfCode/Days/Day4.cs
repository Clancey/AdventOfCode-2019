using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day4 : Day {

		public IEnumerable<int> GeneratePasswords (int starting, int ending)
			=> Enumerable.Range (starting, ending - starting + 1);

		public override void SolvePart1 ()
		{
			var result = GeneratePasswords (356261, 846303).Count (ValidateSimplePassword);
			Console.WriteLine ($"Valid Password Count: {result}");
		}

		public override void SolvePart2 ()
		{
			var test = ValidatePassword (112233);
			var test1 = ValidatePassword (123444);
			var test2 = ValidatePassword (111122);
			var result = GeneratePasswords (356261, 846303).Count (ValidatePassword);
			Console.WriteLine ($"Valid Password Count: {result}");
		}

		bool ValidateSimplePassword (int password)
		{
			var str = password.ToString ();
			if (str.Length != 6)
				return false;
			bool adjacent = false;
			int last = 0;
			foreach (var c in str) {
				var digit = (int)c;
				if (digit < last)
					return false;
				if (!adjacent) {
					adjacent = digit == last;
				}
				last = digit;
			}
			return adjacent;
		}



		bool ValidatePassword (int password)
		{
			var str = password.ToString ();
			if (str.Length != 6)
				return false;
			bool hasExactDouble = false;
			int last = 0;
			int currentRepeat = 0;
			foreach (var c in str) {
				var digit = (int)c;
				if (digit < last)
					return false;
				if (digit == last) {
					currentRepeat++;
				} else if (currentRepeat > 0) {
					if (currentRepeat == 1)
						hasExactDouble = true;
					currentRepeat = 0;
				}
				last = digit;
			}

			return hasExactDouble || currentRepeat == 1;
		}
	}
}
