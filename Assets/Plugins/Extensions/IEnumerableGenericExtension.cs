using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableGenericExtension
{
	public static void Each<T>(this IEnumerable<T> source, Action<T> action)
	{
		foreach(T current in source) {
			action.Invoke(current);
		}
	}

	public static void EachWithIndex<T>(this IEnumerable<T> source, Action<T, int> action)
	{
		int index = 0;
		foreach(T current in source) {
			action.Invoke(current, index);
			index += 1;
		}
	}

	public static T Sample<T>(this IEnumerable<T> source)
	{
		return source.ElementAt(UnityEngine.Random.Range(0, source.Count()));
	}

	public static T[] SampleMany<T>(this IEnumerable<T> source, int n)
	{
		return source.Shuffle().Take(n).ToArray();
	}
}
