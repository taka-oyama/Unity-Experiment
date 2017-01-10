using UnityEngine;
using UnityEngine.Events;

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
			Instantiate(uiPrefab, uiCanvas.transform).Init(stash.Value, Clear);
			onCatch.Invoke(stash.Value);
			this.stash = null;
		}
	}

	public void Clear()
	{
		this.onClear.Invoke();
		this.onCatch.RemoveAllListeners();
		this.onClear.RemoveAllListeners();
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
		public string condition;
		public string trace;
		public LogType type;

		public Info(string condition, string trace, LogType type)
		{
			this.condition = condition;
			this.trace = trace;
			this.type = type;
		}
	}
}
