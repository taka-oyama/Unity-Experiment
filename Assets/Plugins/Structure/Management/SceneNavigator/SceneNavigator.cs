using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class SceneNavigator : SingletonBehaviour<SceneNavigator>
{
	public static object[] TransitionParams { get; private set; }
	static SceneBase Current;
	static Scene previousScene;
	static Coroutine transition;

	void Start()
	{
		SceneManager.activeSceneChanged += OnSceneChanged;
		Current = FindObjectOfType<SceneBase>();
	}

	void OnSceneChanged(Scene previous, Scene current)
	{
		previousScene = previous;
		Current = FindObjectOfType<SceneBase>();
	}

	public static void Back(params object[] args)
	{
		MoveTo(previousScene, args);
	}

	public static void MoveTo(Scene scene, params object[] args)
	{
		if(transition != null) {
			Debug.LogWarningFormat("Canceling previous transition and starting new transition to <{0}>", scene.name);
			Instance.StopCoroutine(transition);
		}
		transition = Instance.StartCoroutine(Instance.MoveToCoroutine(scene, args));
	}

	IEnumerator MoveToCoroutine(Scene scene, params object[] args)
	{
		AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(scene.buildIndex);
		sceneLoader.allowSceneActivation = false;
		yield return StartCoroutine(Current.StartExiting());
		yield return new WaitUntil(() => sceneLoader.progress >= 0.9f);
		sceneLoader.allowSceneActivation = true;
		TransitionParams = args;
		transition = null;
	}

	protected override void OnDestroy()
	{
		SceneManager.activeSceneChanged -= OnSceneChanged;
		base.OnDestroy();
	}
}
