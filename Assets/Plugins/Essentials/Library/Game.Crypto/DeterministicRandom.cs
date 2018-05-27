using UnityEngine;
using Game;

namespace Game.Crypto
{
	public sealed class DeterministicRandom
	{
		readonly System.Random rand;
		readonly int min;
		readonly int max;

		public DeterministicRandom(int min, int max)
		{
			this.rand = new System.Random();
			this.min = min;
			this.max = max;
		}

		public DeterministicRandom(int min, int max, int seed)
		{
			this.rand = new System.Random(seed);
			this.min = min;
			this.max = max;
		}

		public DeterministicRandom(IntRange range) : this(range.min, range.max)
		{
		}

		public DeterministicRandom(IntRange range, int seed) : this(range.min, range.max, seed)
		{
		}

		public int Next()
		{
			return rand.Next(min, max);
		}
	}
}
