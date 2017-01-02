using UnityEngine;
using UnityEditor;

public class DiagnosticsEditorWindow : EditorWindow
{
	string boxMessage;

	[MenuItem("Window/Diagnostics")]
	public static void Window()
	{
		GetWindow<DiagnosticsEditorWindow>("Diagnostics"); 
	} 

	protected virtual void OnGUI()
	{
		EditorGUILayout.HelpBox(boxMessage, MessageType.Info, true);
	}

	protected virtual void OnRecompile(double duration)
	{
		boxMessage = string.Format("Script Compilation: {0}s", duration.ToString("0.00"));
		Repaint();
	}

	protected virtual void OnInspectorUpdate()
	{ 
		DetectRecompile();
	}

	#region Compilation Time
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
	#endregion
}
