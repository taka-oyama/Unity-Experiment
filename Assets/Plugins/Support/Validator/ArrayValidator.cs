using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

internal class ArrayValidator
{
	internal static T[] RemoveDuplicateAndSort<T>(T[] source) where T : MonoBehaviour
	{
		var hashSet = new HashSet<T>();
		for(int i = 0; i < source.Length; i++) {
			if(source[i] == null) {
				continue;
			}
			if(hashSet.Contains(source[i])) {
				Debug.LogErrorFormat("{0} already exists in array!", source[i].name);
				source[i] = null;
				continue;
			}
			hashSet.Add(source[i]);
		}
		int currentCount = source.Length;
		T[] newSource = source.Where(o => o != null).OrderBy(o => o.name).ToArray();
		Array.Resize(ref newSource, currentCount);
		return newSource;
	}
}
