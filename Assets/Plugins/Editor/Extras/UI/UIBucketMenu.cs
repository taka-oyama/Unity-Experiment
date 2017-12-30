using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.SceneManagement;

namespace Game.Editor.Extras
{
	public class UIBucketMenu : MonoBehaviour
	{
		[MenuItem("Extras/UI/Create UIBucket", false, 30)]
		public static void Generate()
		{
			CreateAsset<UIBucket>();
		}

		public static void CreateAsset<T> () where T : ScriptableObject
		{
			string sceneName = SceneManager.GetActiveScene().name;
			string path = AssetDatabase.GetAssetPath(Selection.activeObject);
			path = string.IsNullOrEmpty(path) ? "Assets" : path;
			path = File.Exists(path) ? Path.GetDirectoryName(path) : path;
			path = AssetDatabase.GenerateUniqueAssetPath(string.Format("{0}/{1}{2}.asset", path, sceneName, typeof(T).ToString()));

			T asset = ScriptableObject.CreateInstance<T>();
			AssetDatabase.CreateAsset(asset, path);
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();

			EditorUtility.FocusProjectWindow();
			Selection.activeObject = asset;
		}
	}
}
