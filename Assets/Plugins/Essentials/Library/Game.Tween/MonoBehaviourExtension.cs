using UnityEngine;
using System;

namespace Game.Tween
{
	public static class MonoBehaviourExtension
	{
		public static TweenBehaviour TweenTo(this MonoBehaviour source, TimeSpan span, Action<float> onIterate = null, Action onComplete = null)
		{
			return TweenTo(source, (float)span.TotalSeconds, onIterate, onComplete);
		}

		public static TweenBehaviour TweenTo(this MonoBehaviour source, float seconds, Action<float> onIterate = null, Action onComplete = null)
		{
			return source.gameObject.AddComponent<TweenBehaviour>().Begin(seconds, onIterate, onComplete);
		}
	}
}
