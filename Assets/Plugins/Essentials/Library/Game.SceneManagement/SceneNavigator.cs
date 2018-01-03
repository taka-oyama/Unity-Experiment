using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Game.SceneManagement
{
	public sealed class SceneNavigator : MonoBehaviour
	{
		public static object[] TransitionParams { get; private set; }
		static SceneNavigator instance;
		static SceneBase current;
		static Scene previousScene;
		static Coroutine transition;

		void Start()
		{
			SceneManager.activeSceneChanged += OnSceneChanged;
			instance = this;
			current = FindObjectOfType<SceneBase>();
		}

		void OnSceneChanged(Scene previous, Scene next)
		{
			previousScene = previous;
			current = FindObjectOfType<SceneBase>();
		}

		public static void Back(params object[] args)
		{
			MoveTo(previousScene, args);
		}

		public static void MoveTo(Scene scene, params object[] args)
		{
			if(transition != null) {
				Debug.LogWarningFormat("Canceling previous transition and starting new transition to <{0}>", scene.name);
				instance.StopCoroutine(transition);
			}
			transition = instance.StartCoroutine(instance.MoveToCoroutine(scene, args));
		}

		IEnumerator MoveToCoroutine(Scene scene, params object[] args)
		{
			AsyncOperation sceneLoader = SceneManager.LoadSceneAsync(scene.buildIndex);
			sceneLoader.allowSceneActivation = false;
			yield return current.StartCoroutine(current.StartExiting());
			yield return new WaitUntil(() => sceneLoader.progress >= 0.9f);
			sceneLoader.allowSceneActivation = true;
			TransitionParams = args;
			transition = null;
		}

		void OnDestroy()
		{
			SceneManager.activeSceneChanged -= OnSceneChanged;
		}
	}
}
