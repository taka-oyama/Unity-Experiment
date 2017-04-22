using UnityEngine;
using Game.Tween;
using System;

public static class AudioSourceExtension
{
	public static TweenBehaviour FadeIn(this AudioSource source, float seconds, Action onComplete = null)
	{
		return ToVolume(source, seconds, 0f, source.volume, onComplete);
	}

	public static TweenBehaviour FadeOut(this AudioSource source, float seconds, Action onComplete = null)
	{
		return ToVolume(source, seconds, source.volume, 0f, onComplete);
	}

	public static TweenBehaviour ToVolume(this AudioSource source, float seconds, float start, float end, Action onComplete = null)
	{
		return source.gameObject.AddComponent<TweenBehaviour>().Begin(
			totalSeconds: seconds,
			onIterate: ratio => source.volume = Mathf.Lerp(start, end, ratio),
			onComplete: onComplete,
			destroyOnComplete: true
		);
	}
}
