using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Collections;
using Game.UI;
using Zenject;

namespace Game.SceneManagement
{
	public abstract class SceneBase : MonoBehaviour
	{
		public enum State
		{
			Pending,        // 開始待ち
			Init,			// シーンに入った時の処理
			Load,		    // アセットバンドルのロードはここで
			Prepare,		// ロード後のセットアップはここで
			Run,			// ゲーム実行
			Exiting,		// シーンを抜ける前処理
			Exit,			// シーンを抜ける
		}

		IEnumerator Start()
		{
			ChangeFrameRate(defaultFrameRate);

			ChangeState(State.Init);
			yield return StartCoroutine(Init());

			ChangeState(State.Load);
			yield return StartCoroutine(Load());

			ChangeState(State.Prepare);
			yield return StartCoroutine(Prepare());

			ChangeState(State.Run);
			ChangeFrameRate(runningFrameRate);
			Run();
		}

		internal IEnumerator StartExiting()
		{
			if((int)currentState >= (int)State.Exiting) {
				Debug.LogWarningFormat("Scene<{0}> has already started exiting.", GetType().Name);
				yield break;
			}
			
			ChangeState(State.Exiting);
			ChangeFrameRate(defaultFrameRate);
			yield return StartCoroutine(BeforeExit());

			ChangeState(State.Exit);
			yield return StartCoroutine(Exit());
		}

		#region State Management
		public class StateChangeEvent : UnityEvent<State> {}
		[HideInInspector] public StateChangeEvent onStateChanged = new StateChangeEvent();
		State currentState;

		void ChangeState(State state)
		{
			Log("State: {0} -> {1}", currentState.ToString(), state.ToString());
			this.currentState = state;
			this.onStateChanged.Invoke(state);
		}

		protected virtual IEnumerator Init()
		{
			yield break;
		}

		protected virtual IEnumerator Load()
		{
			yield break;
		}

		protected virtual IEnumerator Prepare()
		{
			yield break;
		}

		protected abstract void Run();

		protected virtual IEnumerator BeforeExit()
		{
			yield break;
		}

		protected virtual IEnumerator Exit()
		{
			yield break;
		}
		#endregion

		#region Frame Rate Management
		const int defaultFrameRate = 30;
		int runningFrameRate = 30;

		/// <summary>
		/// Setting the framerate here will allow the code to set the framerate
		/// only during the Run state and resort to default framerate during 
		/// scene transitions.
		/// </summary>
		protected void SetRunningFrameRate(int newFrameRate)
		{
			this.runningFrameRate = newFrameRate;
		}

		void ChangeFrameRate(int newFrameRate)
		{
			if(newFrameRate != Application.targetFrameRate) {
				Log("Changing Frame Rate to {0}", newFrameRate);
				Application.targetFrameRate = newFrameRate;
			}
		}
		#endregion

		#region UI
		[SerializeField] UICanvas sceneUI;

		public UICanvas UI
		{
			get { return sceneUI; }
		}
		#endregion

		#region Logger
		[SerializeField] bool enableLogging = true;

		protected void Log(string message, params object[] args)
		{
			if(enableLogging) {
				Debug.LogFormat(string.Concat(string.Format("[{0}] ", GetType().Name), message), args);
			}
		}
		#endregion
	}
}
