using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
class CompileTimePostProcessor : EditorWindow
{
	const string outputFormat = "Script compilation time: {0}\n";

	bool isTrackingTime;
	double startTime, finishTime, compileTime;

	void Update()
	{
		if (EditorApplication.isCompiling && !isTrackingTime) {
			startTime = EditorApplication.timeSinceStartup; 
			isTrackingTime = true;
		} else if (!EditorApplication.isCompiling && isTrackingTime) {
			finishTime = EditorApplication.timeSinceStartup; 
			isTrackingTime = false;
			compileTime = finishTime - startTime;
			Debug.Log(string.Format(outputFormat, compileTime.ToString("0.00")));
		}
	}
}
