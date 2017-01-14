﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class ErrorCatcher : SingletonBehaviour<ErrorCatcher>
{
	public bool DidCatch { get; private set; }
	Info? stash;

	[SerializeField] Canvas uiCanvas;
	[SerializeField] UIDebugError uiPrefab;
	[HideInInspector] public UnityEvent<Info> onCatch;
	[HideInInspector] public UnityEvent onClear;

	protected override void Awake()
	{
		base.Awake();
		this.DidCatch = false;
		Application.logMessageReceived += LogHandleCallback;
		Application.logMessageReceivedThreaded += LogHandleCallback;
	}

	void LogHandleCallback(string condition, string trace, LogType type)
	{
		if(type == LogType.Assert || type == LogType.Error || type == LogType.Exception) {
			if(!DidCatch) {
				DidCatch = true;
				stash = new Info(condition, trace, type);
			}
		}
	}

	void Update()
	{
		if(stash.HasValue) {
			Instantiate(uiPrefab, uiCanvas.transform, false).Init(stash.Value, Clear);
			if(onCatch != null) {
				onCatch.Invoke(stash.Value);
			}
			this.stash = null;
		}
	}

	public void Clear()
	{
		if(onClear != null) {
			this.onClear.Invoke();
		}
		if(onCatch != null) {
			this.onClear.RemoveAllListeners();
			this.onCatch.RemoveAllListeners();
		}
		this.DidCatch = false;
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
