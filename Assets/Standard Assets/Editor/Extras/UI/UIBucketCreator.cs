using UnityEngine;
using UnityEditor;
using System.IO;

public class UIBucketCreator : MonoBehaviour
{
	[MenuItem("Extras/UI/Create UIBucket", false, 10)]
	public static void Generate()
	{
		CreateAsset<UIBucket>();
	}

	public static void CreateAsset<T> () where T : ScriptableObject
	{
		string path = AssetDatabase.GetAssetPath(Selection.activeObject);
		path = string.IsNullOrEmpty(path) ? "Assets" : path;
		path = File.Exists(path) ? Path.GetDirectoryName(path) : path;
		path = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/New{1}.asset", path, typeof(T).ToString()));

		T asset = ScriptableObject.CreateInstance<T>();
		AssetDatabase.CreateAsset(asset, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}