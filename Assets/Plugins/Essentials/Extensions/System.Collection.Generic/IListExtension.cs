using System.Collections.Generic;

public static class IListExtension
{
	public static T RemoveRandom<T>(this IList<T> source)
	{
		if(source.IsEmpty()) {
			return default(T);
		}
		int index = UnityEngine.Random.Range(0, source.Count);
		T value = source[index];
		source.RemoveAt(index);
		return value;
	}
}
