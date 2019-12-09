using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day1 : Day {

		public int CalulateFuelCost (int mass) => (mass / 3) - 2;

		public int CalculateFuel2 (int mass)
		{
			var fuel = CalulateFuelCost (mass);
			var totalFuel = fuel;
			bool hasMore = true;
			while (hasMore) {
				var fuelCost = CalulateFuelCost (fuel);
				if (fuelCost < 0)
					break;
				totalFuel += fuelCost;
				fuel = fuelCost;
			}
			return totalFuel;
		}

		public override void SolvePart1 ()
		{
			var result = ReadInputLinesAsInt ().Sum (CalulateFuelCost);
			Console.WriteLine ($"Fuel Cost: {result}");
		}

		public override void SolvePart2 ()
		{
			var result = ReadInputLinesAsInt ().Sum (CalculateFuel2);
			Console.WriteLine ($"Fuel Cost: {result}");

		}
	}
}
