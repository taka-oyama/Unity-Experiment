using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class FindReferencesInProject : EditorWindow
{
	[MenuItem("Assets/Find References In Project", false, 25)]
	static void OnSearchForReferences()
	{
		var matches = new List<UnityEngine.Object>();
		int iid = Selection.activeInstanceID;
		string refPath = AssetDatabase.GetAssetPath(iid);
		string refName = System.IO.Path.GetFileNameWithoutExtension(refPath);
		Object[] allObjects = Resources.FindObjectsOfTypeAll(typeof(Object));
		Object[] placeHolder = new Object[1];

		Debug.LogFormat("Searching all references for {0}\n", refPath);

		if(!AssetDatabase.IsMainAsset(iid)) {
			Debug.LogErrorFormat("Asset: {0} not found", refPath);
			return;
		}

		foreach (Object target in allObjects) {
			placeHolder[0] = target;
			Object[] depndencyObjects = EditorUtility.CollectDependencies(placeHolder);
			string[] dependancies = depndencyObjects.Select(d => d.name).Distinct().ToArray();
			foreach(string dependancy in dependancies) {
				if(string.Compare(dependancy, refName) == 0) {
					if(AssetDatabase.IsMainAsset(target)) {
						string objectPath = AssetDatabase.GetAssetPath(target);
						if(objectPath != refPath && !string.IsNullOrEmpty(objectPath)) {
							Debug.Log(objectPath + "\n", target);
							matches.Add(target);
						}
					}
				}
			}
		}
		Selection.objects = matches.ToArray();
		matches.Clear();
	}


}