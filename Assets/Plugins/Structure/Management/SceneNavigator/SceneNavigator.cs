using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class SceneNavigator : SingletonBehaviour<SceneNavigator>
{
	public SceneBase Current { get; private set; }
	public object[] TransitionParams { get; private set; }
	Scene previousScene;
	Coroutine transition;
	string nextSceneName;

	void Start()
	{
		SceneManager.activeSceneChanged += OnSceneChanged;
		this.Current = GameObject.FindObjectOfType<SceneBase>();
	}

	void OnSceneChanged(Scene previous, Scene current)
	{
		this.previousScene = previous;
		this.Current = GameObject.FindObjectOfType<SceneBase>();
	}

	public void Back(params object[] args)
	{
		MoveTo(previousScene, args);
	}

	public void MoveTo(Scene scene, params object[] args)
	{
		MoveTo(scene.name, args);
	}

	public void MoveTo(string sceneName, params object[] args)
	{
		if(transition != null) {
			Debug.LogWarningFormat("Canceling transition to <{0}> and starting new transition to <{1}>", nextSceneName, sceneName);
			this.nextSceneName = null;
			StopCoroutine(transition);
		}
		this.nextSceneName = sceneName;
		this.transition = StartCoroutine(MoveToCoroutine(sceneName, args));
	}

	IEnumerator MoveToCoroutine(string sceneName, params object[] args)
	{
		AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneName);
		sceneLoader.allowSceneActivation = false;
		yield return StartCoroutine(Current.StartExiting());
		yield return new WaitUntil(() => sceneLoader.progress >= 0.9f);
		sceneLoader.allowSceneActivation = true;
		this.TransitionParams = args;
		this.nextSceneName = null;
		this.transition = null;
	}

	protected override void OnDestroy()
	{
		SceneManager.activeSceneChanged -= OnSceneChanged;
		base.OnDestroy();
	}
}
