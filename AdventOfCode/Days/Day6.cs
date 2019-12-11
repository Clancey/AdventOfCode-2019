using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day6 : Day {
		public Day6 ()
		{
		}
		public List<(string parent, string child)> GetStarMap () => this.ReadInputLines ().Select (Split).ToList ();

		public List<string> AllOrbitalItems = new List<string> ();
		public Dictionary<string, string> ChildToParentDictionary = new Dictionary<string, string> ();

		public void PopulateAllData ()
		{
			var data = GetStarMap ();
			AllOrbitalItems = data.SelectMany (x => new [] { x.child, x.parent }).Distinct ().Where (x => x != "COM").ToList ();
			ChildToParentDictionary = data.ToDictionary (x => x.child, x => x.parent);
		}
		public int GetOrbitLength (string planet, string endingPlanet = "COM")
		{
			var currentPlanet = planet;
			int hop = 0;
			while (currentPlanet != endingPlanet) {
				currentPlanet = ChildToParentDictionary [currentPlanet];
				hop++;
			}
			return hop;
		}
		public int GetTotalLength () => AllOrbitalItems.Sum (x=> GetOrbitLength(x));

		(string parent, string child) Split (string input)
		{
			var pairs = input.Split (')');
			return (pairs [0], pairs [1]);
		}
		public override void SolvePart1 ()
		{
			PopulateAllData ();
			var answer = GetTotalLength ();
			Console.WriteLine ($"Answer: {answer} ");
		}

		List<string> GetTree(string planet)
		{
			List<string> hops = new List<string> ();
			var currentPlanet = planet;
			while (currentPlanet != "COM") {
				currentPlanet = ChildToParentDictionary [currentPlanet];
				hops.Add (currentPlanet);
			}
			hops.Reverse ();
			return hops;
		}

		public override void SolvePart2 ()
		{
			var santaList = GetTree ("SAN");
			var meList = GetTree ("YOU");
			
			var lastHop = "COM";
			foreach(var hop in santaList) {
				if (meList.Contains (hop))
					lastHop = hop;
				else break;
			}
			var meLenthg = GetOrbitLength ("YOU", lastHop);
			var santaLength = GetOrbitLength ("SAN", lastHop);
			var answer = meLenthg + santaLength - 2;
			Console.WriteLine ($"Answer: {answer}");
			//throw new NotImplementedException ();
		}
	}
}
