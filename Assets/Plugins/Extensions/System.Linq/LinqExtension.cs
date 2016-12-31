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
	}
}