using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableGenericExtension
{
	public static void Each<T>(this IEnumerable<T> source, Action<T> action)
	{
		using(var enumerator = source.GetEnumerator()) {
			while(enumerator.MoveNext()) {
				action(enumerator.Current);
			}
		}
	}

	public static void EachWithIndex<T>(this IEnumerable<T> source, Action<T, int> action)
	{
		using(var enumerator = source.GetEnumerator()) {
			int index = 0;
			while(enumerator.MoveNext()) {
				action(enumerator.Current, index);
				index += 1;
			}
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
