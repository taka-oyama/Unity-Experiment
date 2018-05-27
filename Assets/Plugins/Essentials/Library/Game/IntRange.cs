using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace Game
{
	public struct IntRange : IEnumerable<int>
	{
		public int Min;
		public int Max;

		public IntRange(int min, int max)
		{
			this.Min = min;
			this.Max = max;
		}

		public IEnumerator<int> GetEnumerator()
		{
			return Enumerable.Range(Min, Max - Min + 1).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public static IntRange Inclusive(int min, int max)
		{
			if(min > max) {
				throw new ArgumentOutOfRangeException(string.Format("[Invalid Range] min:{0} | max:{1}", min, max));
			}
			return new IntRange(min, max);
		}

		public static IntRange Exclusive(int min, int max)
		{
			int exMax = max - 1;
			if(min > exMax) {
				throw new ArgumentOutOfRangeException(string.Format("[Invalid Range] min:{0} | max:{1}", min, exMax));
			}
			return new IntRange(min, exMax);
		}
	}
}
