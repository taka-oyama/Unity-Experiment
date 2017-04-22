using System;
using System.Linq;

public static class GenericExtension
{
	public static bool IsNull<T>(this T source) where T : class
	{
		return source == null;
	}

	public static bool IsNull<T>(this T? source) where T : struct
	{
		return !source.HasValue;
	}

	public static bool IsNotNull<T>(this T source) where T : class
	{
		return source != null;
	}

	public static bool IsNotNull<T>(this T? source) where T : struct
	{
		return source.HasValue;
	}

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
