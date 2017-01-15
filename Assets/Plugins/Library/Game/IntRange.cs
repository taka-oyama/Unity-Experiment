using UnityEngine.Assertions;

namespace Game
{
	public struct IntRange
	{
		public int min;
		public int max;

		public IntRange(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		public static IntRange Inclusive(int min, int max)
		{
			Assert.IsTrue(min <= max, string.Format("[Invalid Range] min:{0} | max:{1}", min, max));
			return new IntRange(min, max);
		}

		public static IntRange Exclusive(int min, int max)
		{
			int exMax = max - 1;
			Assert.IsTrue(min <= exMax, string.Format("[Invalid Range] min:{0} | max:{1}", min, exMax));
			return new IntRange(min, exMax);
		}
	}
}
