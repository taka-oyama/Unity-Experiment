using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public sealed class SceneCoordinator : GlobalBehaviour<SceneCoordinator>
{
	public SceneBase Active { get; private set; }
	public object[] TransitionParams { get; private set; }
	Scene previousScene;

	void Start()
	{
		SceneManager.activeSceneChanged += OnSceneChanged;
	}

	void OnSceneChanged(Scene previous, Scene current)
	{
		this.previousScene = previous;
		this.Active = GameObject.FindObjectOfType<SceneBase>();
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
		StartCoroutine(MoveToCoroutine(sceneName, args));
	}

	IEnumerator MoveToCoroutine(string sceneName, params object[] args)
	{
		AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(sceneName);
		sceneLoader.allowSceneActivation = false;
		yield return StartCoroutine(Active.StartExiting());
		yield return new WaitUntil(() => sceneLoader.progress >= 0.9f);
		sceneLoader.allowSceneActivation = true;
		this.TransitionParams = args;
	}

	protected override void OnDestroy()
	{
		SceneManager.activeSceneChanged -= OnSceneChanged;
		base.OnDestroy();
	}
}
