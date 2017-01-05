using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public abstract class SceneBase : MonoBehaviour
{
	public enum State
	{
		Enter,			// シーンに入った時の処理
		LoadAssets,		// アセットバンドルのロードはここで
		LoadData,		// データのダウンロードはここで
		Prepare,		// ロード後のセットアップはここで
		Run,			// ゲーム実行
		BeforeExit,		// シーンを抜ける前処理
		Exit,			// シーンを抜ける
	}

	void Awake()
	{
		ChangeState(State.Enter);
	}

	IEnumerator Start()
	{
		ChangeFrameRate(defaultFrameRate);
		yield return StartCoroutine(Enter());

		ChangeState(State.LoadAssets);
		yield return StartCoroutine(LoadAssets());

		ChangeState(State.LoadData);
		yield return StartCoroutine(LoadData());

		ChangeState(State.Prepare);
		yield return StartCoroutine(Prepare());

		ChangeState(State.Run);
		ChangeFrameRate(runningFrameRate);
		Run();
	}

	internal IEnumerator StartExiting()
	{
		ChangeState(State.BeforeExit);
		ChangeFrameRate(defaultFrameRate);
		yield return StartCoroutine(BeforeExit());

		ChangeState(State.Exit);
		yield return StartCoroutine(Exit());
	}

	#region State Management
	public State CurrentState
	{
		get;
		private set;
	}

	void ChangeState(State state)
	{
		this.CurrentState = state;
		Log(string.Concat("State Changed to ", state.ToString()));
	}

	protected virtual IEnumerator Enter()
	{
		yield break;
	}

	protected virtual IEnumerator LoadAssets()
	{
		yield break;
	}

	protected virtual IEnumerator LoadData()
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

	void SetRunningFrameRate(int newFrameRate)
	{
		this.runningFrameRate = newFrameRate;
	}

	void ChangeFrameRate(int newFrameRate)
	{
		Application.targetFrameRate = newFrameRate;
	}
	#endregion

	#region Logger
	protected void Log(string message, params object[] args)
	{
		Debug.LogFormat(string.Concat(string.Format("[{0}]", GetType().Name), message), args);
	}
	#endregion
}
