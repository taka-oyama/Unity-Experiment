using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class SceneNavigator : SingletonBehaviour<SceneNavigator>
{
	public static SceneBase Current { get; private set; }
	public static object[] TransitionParams { get; private set; }
	static Scene previousScene;
	static Coroutine transition;
	static string nextSceneName;

	void Start()
	{
		SceneManager.activeSceneChanged += OnSceneChanged;
		Current = GameObject.FindObjectOfType<SceneBase>();
	}

	void OnSceneChanged(Scene previous, Scene current)
	{
		previousScene = previous;
		Current = GameObject.FindObjectOfType<SceneBase>();
	}

	public static void Back(params object[] args)
	{
		MoveTo(previousScene, args);
	}

	public static void MoveTo(Scene scene, params object[] args)
	{
		MoveTo(scene.name, args);
	}

	public static void MoveTo(string sceneName, params object[] args)
	{
		if(transition != null) {
			Debug.LogWarningFormat("Canceling transition to <{0}> and starting new transition to <{1}>", nextSceneName, sceneName);
			nextSceneName = null;
			I.StopCoroutine(transition);
		}
		nextSceneName = sceneName;
		transition = I.StartCoroutine(I.MoveToCoroutine(sceneName, args));
	}

	IEnumerator MoveToCoroutine(string sceneName, params object[] args)
	{
		AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneName);
		sceneLoader.allowSceneActivation = false;
		yield return StartCoroutine(Current.StartExiting());
		yield return new WaitUntil(() => sceneLoader.progress >= 0.9f);
		sceneLoader.allowSceneActivation = true;
		TransitionParams = args;
		nextSceneName = null;
		transition = null;
	}

	protected override void OnDestroy()
	{
		SceneManager.activeSceneChanged -= OnSceneChanged;
		base.OnDestroy();
	}
}
