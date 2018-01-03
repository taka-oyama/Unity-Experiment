using UnityEngine;
using System;
using System.Collections;

namespace Game.Tween
{
	public class TweenBehaviour : MonoBehaviour
	{
		enum State { Pending, Playing, Paused, Finished, Stopped }

		State state;
		float elapsedTime;
		float totalSeconds;
		Action<float> onIterate;
		Action onComplete;
		Coroutine routine;

		public bool destroyOnComplete;

		public bool IsPlaying  { get { return state == State.Playing; } }
		public bool IsPaused   { get { return state == State.Paused; } }
		public bool IsFinished { get { return state == State.Finished; } }

		void Awake()
		{
			this.state = State.Pending;
		}

		public TweenBehaviour Begin(float totalSeconds, Action<float> onIterate = null, Action onComplete = null, bool destroyOnComplete = true)
		{
			this.state = State.Playing;
			this.elapsedTime = 0f;
			this.totalSeconds = totalSeconds;
			this.onIterate = onIterate;
			this.onComplete = onComplete;
			this.routine = StartCoroutine(TweenToCoroutine());
			this.destroyOnComplete = destroyOnComplete;
			return this;
		}

		public TweenBehaviour Begin(TimeSpan span, Action<float> onIterate = null, Action onComplete = null, bool destroyOnComplete = true)
		{
			return Begin((float)span.TotalSeconds, onIterate, onComplete, destroyOnComplete);
		}

		public void Pause()
		{
			if(state == State.Playing) {
				this.state = State.Paused;
			}
		}

		public void UnPause()
		{
			if(state == State.Paused) {
				this.state = State.Playing;
			}
		}

		public void Cancel(bool andDestroy = false)
		{
			if(routine != null) {
				StopCoroutine(routine);
			}
			if(andDestroy) {
				Destroy(this);
			}
		}

		IEnumerator TweenToCoroutine()
		{
			yield return new WaitWhile(() => {
				if(state == State.Paused) {
					return true;
				}
				this.elapsedTime += Time.deltaTime;
				float rate = Mathf.Clamp01(elapsedTime / totalSeconds);
				if(onIterate != null && onIterate.Target != null) {
					onIterate(rate);
				}
				return rate < 1f;
			});

			this.state = State.Finished;
			this.routine = null;
			this.onIterate = null;

			if(onComplete != null && onComplete.Target != null) {
				onComplete();
			}
			this.onComplete = null;
			if(destroyOnComplete) {
				Destroy(this);
			}
		}
	}
}
