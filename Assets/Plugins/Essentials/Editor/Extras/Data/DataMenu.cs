using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace Game.Editor.Extras
{
	public class DataMenu : MonoBehaviour
	{
		[MenuItem("Extras/Data/Open Cache Path", false, 10)]
		public static void OpenCachePath()
		{
			string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string bundleId = Application.identifier;
			string cachePath = string.Format("{0}/Library/Caches/Unity/{1}", rootPath, bundleId);
			if(Directory.Exists(cachePath)) {
				EditorUtility.RevealInFinder(cachePath);
			} else {
				Debug.LogWarningFormat("Cache Path: {0} does not exist.", cachePath);
			}
		}

		[MenuItem("Extras/Data/Open Data Path", false, 10)]
		public static void OpenDataPath()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);
		}

		[MenuItem("Extras/Data/Open Temp Path", false, 10)]
		public static void OpenTempPath()
		{
			EditorUtility.RevealInFinder(Application.temporaryCachePath);
		}

		[MenuItem("Extras/Data/Delete Player Data", false, 60)]
		public static void DeletePlayerData()
		{
			PlayerPrefs.DeleteAll();
			ClearDataInPath(Application.persistentDataPath);
		}

		[MenuItem("Extras/Data/Clear Cache", false, 60)]
		public static void ClearCache()
		{
			Caching.ClearCache();
			ClearDataInPath(Application.temporaryCachePath);
		}

		static void ClearDataInPath(string path)
		{
			DirectoryInfo directory = new DirectoryInfo(path);
			foreach(FileInfo file in directory.GetFiles()) file.Delete();
			foreach(DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
		}

	}
}
