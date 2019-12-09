using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day8 {

		List<int> ParsePixels(string filePath)
		{
			var stringData = File.ReadAllText (filePath);
			var pixels = stringData.Select (x=> int.Parse(x.ToString())).ToList ();
			return pixels;
		}
		List<Layer> ParseLayers (List<int> pixels, int width, int height)
		{
			var pixelLength = width * height;
			var layerCount = pixels.Count / pixelLength;
			var layers = Enumerable.Range (0, layerCount).Select (x => pixels.Skip (pixelLength * x).Take (pixelLength))
				.Select (x => new Layer { Pixels = x.ToList () }).ToList ();
			return layers;
			//pixels.Take(pixels)
		}

		public int Solve (string filePath, int width, int height)
		{
			var pixels = ParsePixels (filePath);
			var layers = ParseLayers (pixels, width, height);
			var answerLayer = layers.OrderBy (x => x.NumberOfZeros ()).First ();
			return answerLayer.Value;
		}
		

		public class Layer {

			public List<int> Pixels = new List<int> ();
			int zeroCount = -1;
			public int NumberOfZeros()
			{
				if (zeroCount == -1)
					zeroCount = Pixels.Count (x => x == 0);
				return zeroCount;
			}

			public int Value => Pixels.Count (x => x == 1) * Pixels.Count (x => x == 2);
		}
	}
}
