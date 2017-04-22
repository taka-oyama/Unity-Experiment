using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

public class DiagnosticsEditorWindow : EditorWindow
{
	static bool isInitialized = false;
	static int uiPadding = 10;
	static Vector2 scrollPosition = Vector2.zero;

	Action onGUI;
	Action onInspectorUpdate;
	Action<double> onRecompile;

	[MenuItem("Window/Diagnostics")]
	public static void Window()
	{
		GetWindow<DiagnosticsEditorWindow>("Diagnostics"); 
	}

	void Awake()
	{ 
		Initialize();
	}

	void Initialize()
	{
		// Add your setups here
		SetupCompilationTime();
		SetupRunInBackground();
		SetupAudioListener();
		SetupTimeScale();
		SetupOpenFolderInExplorer();
		SetupClearContents();

		onInspectorUpdate += DetectRecompile;
		isInitialized = true;
	}

	void OnGUI()
	{
		if(!isInitialized) Initialize();
		scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
		if(onGUI != null) onGUI.Invoke();
		EditorGUILayout.EndScrollView();
	}

	void OnInspectorUpdate()
	{
		if(onInspectorUpdate != null) onInspectorUpdate.Invoke();
		Repaint(); 
	}

	void OnRecompile(double duration)
	{
		if(onRecompile != null) onRecompile.Invoke(duration);
	}

	bool isTrackingTime;
	double startTime, finishTime;
	void DetectRecompile()
	{
		if (EditorApplication.isCompiling && !isTrackingTime) {
			startTime = EditorApplication.timeSinceStartup; 
			isTrackingTime = true;
		} else if (!EditorApplication.isCompiling && isTrackingTime) {
			finishTime = EditorApplication.timeSinceStartup; 
			isTrackingTime = false;
			OnRecompile(finishTime - startTime);
		}
	}

	#region Run In Background
	void SetupRunInBackground()
	{
		onGUI += () => {
			GUILayout.Space(uiPadding);
			Application.runInBackground = EditorGUILayout.Toggle("Run In Background", Application.runInBackground);
			GUILayout.Space(uiPadding);
		};
	}
	#endregion

	#region Open Folder In Explorer
	void SetupOpenFolderInExplorer()
	{
		int selection = -1;
		string[] options = new [] { "Cache Path", "Temp Path", "Data Path" };
		onGUI += () => {
			GUILayout.Space(uiPadding);
			GUILayout.Label("Show Paths In Explorer");
			if((selection = GUILayout.Toolbar(-1, options)) != -1) {
				switch(selection) {
				case 0:
					string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					string bundleId = Application.identifier;
					string cachePath = string.Format("{0}/Library/Caches/Unity/{1}", rootPath, bundleId);
					if(Directory.Exists(cachePath)) {
						EditorUtility.RevealInFinder(cachePath);
					} else {
						Debug.LogWarningFormat("Cache Path: {0} does not exist.", cachePath);
					}
					break;
				case 1:
					EditorUtility.RevealInFinder(Application.temporaryCachePath);
					break;
				case 2:
					EditorUtility.RevealInFinder(Application.persistentDataPath);
					break;
				}
			}
			GUILayout.Space(uiPadding);
		};
	}
	#endregion

	#region Clear Contents
	void SetupClearContents()
	{
		int selection = -1;
		string[] options = new [] { "PlayerPrefs", "Cache", "Temp", "Everything" };
		onGUI += () => {
			GUILayout.Space(uiPadding);
			GUILayout.Label("Clear User Data");
			if((selection = GUILayout.Toolbar(-1, options)) != -1) {
				switch(selection) {
				case 0:
					if(EditorUtility.DisplayDialog(options[selection], GenerateClearContentQuestion(" - call PlayerPrefs.DeleteAll()"), "Clear All", "Cancel")) {
						PlayerPrefs.DeleteAll();
					}
					break;
				case 1:
					if(EditorUtility.DisplayDialog(options[selection], GenerateClearContentQuestion(" - call Caching.CleanCache()"), "Clear All", "Cancel")) {
						Caching.CleanCache();
					}
					break;
				case 2:
					if(EditorUtility.DisplayDialog(options[selection], GenerateClearContentQuestion(" - clear temporaryCachePath"), "Clear All", "Cancel")) {
						ClearDataInPath(Application.temporaryCachePath);
					}
					break;
				case 3:
					if(EditorUtility.DisplayDialog(options[selection], GenerateClearContentQuestion(" - call PlayerPrefs.DeleteAll()", " - call Caching.CleanCache()", " - clear temporaryCachePath", " - clear persistentDataPath"), "Clear All", "Cancel")) {
						PlayerPrefs.DeleteAll();
						ClearDataInPath(Application.temporaryCachePath);
						ClearDataInPath(Application.persistentDataPath);
						Caching.CleanCache();
					}
					break;
				}
			}
			GUILayout.Space(uiPadding);
		};
	}

	string GenerateClearContentQuestion(params string[] activities)
	{
		var list = new List<string>();
		list.Add("This will do the following\n");
		list.AddRange(activities);
		list.Add("\nAre you sure you want to do this?\n");
		return string.Join("\n", list.ToArray());
	}

	void ClearDataInPath(string path)
	{
		DirectoryInfo directory = new DirectoryInfo(path);
		foreach(FileInfo file in directory.GetFiles()) file.Delete();
		foreach(DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
	}
	#endregion

	#region Compilation Time
	void SetupCompilationTime()
	{
		string compileTimeFormat = "Script Compilation: {0}s";
		string compliationTimeMessage = string.Format(compileTimeFormat, 0);

		onGUI += () => {
			GUILayout.Space(uiPadding);
			EditorGUILayout.HelpBox(compliationTimeMessage, MessageType.Info, true);
			GUILayout.Space(uiPadding);
		};

		onRecompile += duration => {
			compliationTimeMessage = string.Format(compileTimeFormat, duration.ToString("0.00"));
		};
	}
	#endregion

	#region AudioListener
	void SetupAudioListener()
	{
		onGUI += () => {
			GUILayout.Space(uiPadding);
			AudioListener.volume = EditorGUILayout.Slider("Audio Listener", AudioListener.volume, 0.0f, 1.0f);
			GUILayout.Space(uiPadding);
		};
	}
	#endregion

	#region TimeScale
	void SetupTimeScale()
	{
		onGUI += () => {
			GUILayout.Space(uiPadding);
			Time.timeScale = EditorGUILayout.Slider("Time Scale", Time.timeScale, 0.0f, 10.0f);
			GUILayout.Space(uiPadding);
		};
	}
	#endregion
}
