using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public sealed class ErrorCatcher : SingletonBehaviour<ErrorCatcher>
{
	Info? stash;

	[SerializeField] Canvas canvas;
	[SerializeField] UIDebugError uiDebugPrefab;
	[HideInInspector] public UnityEvent<Info> onCatch;
	[HideInInspector] public UnityEvent onClear;

	protected override void Awake()
	{
		base.Awake();
		canvas.gameObject.SetActive(false);
		Application.logMessageReceived += LogHandleCallback;
		Application.logMessageReceivedThreaded += LogHandleCallback;
	}

	void LogHandleCallback(string condition, string trace, LogType type)
	{
		if(type == LogType.Assert || type == LogType.Error || type == LogType.Exception) {
			if(!canvas.isActiveAndEnabled) {
				stash = new Info(condition, trace, type);
			}
		}
	}

	void Update()
	{
		if(stash.HasValue) {
			canvas.gameObject.SetActive(true);
			Instantiate(uiDebugPrefab, canvas.transform, false).Init(stash.Value, Clear);
			if(onCatch != null) {
				onCatch.Invoke(stash.Value);
			}
			this.stash = null;
		}
	}

	public void Clear()
	{
		canvas.gameObject.SetActive(false);
		if(onClear != null) {
			this.onClear.Invoke();
			this.onClear.RemoveAllListeners();
		}
		if(onCatch != null) {
			this.onCatch.RemoveAllListeners();
		}
		this.stash = null;
	}

	protected override void OnDestroy()
	{
		Application.logMessageReceived -= LogHandleCallback;
		Application.logMessageReceivedThreaded -= LogHandleCallback;
		base.OnDestroy();
	}

	public struct Info
	{
		public string sceneName;
		public string condition;
		public string trace;
		public LogType type;
		public DateTime time;

		public Info(string condition, string trace, LogType type)
		{
			this.sceneName = SceneManager.GetActiveScene().name;
			this.condition = condition;
			this.trace = trace;
			this.type = type;
			this.time = DateTime.Now;
		}
	}
}
