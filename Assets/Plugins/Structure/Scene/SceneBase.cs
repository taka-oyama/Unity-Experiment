using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;

public abstract class SceneBase : GlobalBehaviour<SceneBase>
{
	public enum State
	{
		Pending,        // 開始待ち
		Init,			// シーンに入った時の処理
		Load,		    // アセットバンドルのロードはここで
		Prepare,		// ロード後のセットアップはここで
		Run,			// ゲーム実行
		BeforeExit,		// シーンを抜ける前処理
		Exit,			// シーンを抜ける
	}

	protected override void Awake()
	{
		base.Awake();
		GameInitializer.CreateIfNotExist();
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

	/// <summary>
	/// Starts the exiting process.
	/// Should only be called from Scene Navigator.
	/// </summary>
	internal IEnumerator StartExiting()
	{
		if((int)currentState >= (int)State.BeforeExit) {
			Debug.LogWarningFormat("Scene<{0}> has already started exiting.", GetType().Name);
			yield break;
		}
		
		ChangeState(State.BeforeExit);
		ChangeFrameRate(defaultFrameRate);
		yield return StartCoroutine(BeforeExit());

		ChangeState(State.Exit);
		yield return StartCoroutine(Exit());
	}

	#region State Management
	[SerializeField][ReadOnly] State currentState;

	void ChangeState(State state)
	{
		Log("State: {0} -> {1}", currentState.ToString(), state.ToString());
		this.currentState = state;
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
	protected void Log(string message, params object[] args)
	{
		Debug.LogFormat(string.Concat(string.Format("[{0}] ", GetType().Name), message), args);
	}
	#endregion
}
