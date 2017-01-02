using System;
using System.Linq;

public static class GenericExtension
{
	public static bool IsIn<T>(this T source, params T[] list)
	{
		if(source == null) throw new ArgumentNullException("source");
		return list.Contains(source);
	}

	public static bool IsBetween<T>(this T actual, T lower, T upper) where T : IComparable<T>
	{
		return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) < 0;
	}
}
