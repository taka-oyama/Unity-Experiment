using System.Collections.Generic;

public static class ICollectionGenericExtension
{
	public static bool IsEmpty<T>(this ICollection<T> collection)
	{
		return collection.Count == 0;
	}

	public static bool IsNotEmpty<T>(this ICollection<T> collection)
	{
		return collection.Count != 0;
	}
}
