using System;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day1 : Day {

		public int Solve () => ReadInputLinesAsInt ().Sum (CalulateFuelCost);

		public int Solve2 () => ReadInputLinesAsInt ().Sum (CalculateFuel2);

		public int CalulateFuelCost (int mass) => (mass / 3) - 2;

		public int CalculateFuel2(int mass)
		{
			var fuel = CalulateFuelCost (mass);
			var totalFuel = fuel;
			bool hasMore = true;
			while(hasMore) {
				var fuelCost = CalulateFuelCost (fuel);
				if (fuelCost < 0)
					break;
				totalFuel += fuelCost;
				fuel = fuelCost;
			}
			return totalFuel;
		}
	}
}
