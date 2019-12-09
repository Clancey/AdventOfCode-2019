using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days {
	public class Day8 : Day {

		List<int> _pixels;
		List<int> ParsePixels ()
		{
			if (_pixels == null) {
				var stringData = this.ReadInputString ();
				_pixels = stringData.Select (x => int.Parse (x.ToString ())).ToList ();
			}
			return _pixels;
		}

		List<Layer> _layers;
		List<Layer> ParseLayers (List<int> pixels, int width, int height)
		{
			if (_layers == null) {
				var pixelLength = width * height;
				var layerCount = pixels.Count / pixelLength;
				_layers = Enumerable.Range (0, layerCount).Select (x => pixels.Skip (pixelLength * x).Take (pixelLength))
					.Select (x => new Layer { Pixels = x.ToList (), Width = width, Height = height }).ToList ();
			}
			return _layers;
		}

		public int Solve (int width = 25, int height = 6)
		{
			var layers = GetLayers (width, height);
			var answerLayer = layers.OrderBy (x => x.NumberOfZeros ()).First ();
			return answerLayer.Value;
		}
		public List<Layer> GetLayers (int width = 25, int height = 6) => ParseLayers (ParsePixels (), width, height);

		public List<string> GetRows (int width = 25, int height = 6)
		{
			var layers = GetLayers (width, height);

			var length = width * height;
			var mergedPixels = new List<int> ();
			for (var i = 0; i < length; i++) {
				int pixel = 2;
				try {
					pixel = layers.Select (x => x.Pixels [i]).First (x => x != 2);
				} catch (Exception ex) {
					pixel = 2;
				}
				mergedPixels.Add (pixel);
			}
			return Enumerable.Range (0, height)
				.Select (x => String.Join ("", mergedPixels.Skip (x * width)
				.Take (width)).Replace ("0", " ")).ToList ();
		}

		public override void SolvePart1 ()
		{
			var height = 6;
			var width = 25;
			var layers = GetLayers (width, height);
			var answerLayer = layers.OrderBy (x => x.NumberOfZeros ()).First ();
			Console.Write($"Value: {answerLayer.Value}");
		}

		public override void SolvePart2 ()
		{
			foreach (var row in GetRows ())
				Console.WriteLine (row);
		}

		public class Layer {

			public List<int> Pixels = new List<int> ();
			int zeroCount = -1;
			public int Width { get; set; } = 25;
			public int Height { get; set; } = 6;
			public int NumberOfZeros ()
			{
				if (zeroCount == -1)
					zeroCount = Pixels.Count (x => x == 0);
				return zeroCount;
			}

			public int Value => Pixels.Count (x => x == 1) * Pixels.Count (x => x == 2);

			public List<string> GetPrintString () => Enumerable.Range(0, Height)
				.Select (x => String.Join ("", Pixels.Skip (x* Width)
				.Take (Width)).Replace ("0", " ").Replace("2"," ")).ToList ();



		}
	}
}
