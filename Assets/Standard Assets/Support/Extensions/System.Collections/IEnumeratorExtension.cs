using UnityEngine;

namespace System.Collections
{
	public static class IEnumeratorExtension
	{
		public static Coroutine StartCoroutineOn<T>(this T source, MonoBehaviour targetBehaviour) where T : IEnumerator
		{
			return targetBehaviour.StartCoroutine(source);
		}
	}
}
