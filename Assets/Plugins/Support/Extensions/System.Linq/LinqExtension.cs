using System.Collections.Generic;
using UnityEngine;

namespace System.Linq
{
	public static class LinqExtension
	{
		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
		{
			List<T> buffer = source.ToList();
			for(int i = 0; i < buffer.Count; i++) {
				int j = UnityEngine.Random.Range(i, buffer.Count);
				yield return buffer[j];
				buffer[j] = buffer[i];
			}
		}

		public static IEnumerable<T> Paginate<T>(this IEnumerable<T> source, int page, int size)
		{
			if(page < 1) throw new ArgumentException("Invalid Paginate page. Expected: > 0. Given: " + page.ToString());
			if(size < 1) throw new ArgumentException("Invalid Paginate size. Expected: > 0. Given: " + size.ToString());
			return source.Skip(--page * size).Take(size);
		}
	}
}