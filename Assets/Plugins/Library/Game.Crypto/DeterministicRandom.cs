using UnityEngine;
using Game;

namespace Game.Crypto
{
	public sealed class DeterministicRandom
	{
		System.Random rand;
		int min;
		int max;

		public DeterministicRandom(int seed, IntRange range)
		{
			this.rand = new System.Random(seed);
			this.min = range.min;
			this.max = range.max;
		}

		public int Next()
		{
			return rand.Next(min, max);
		}
	}
}
