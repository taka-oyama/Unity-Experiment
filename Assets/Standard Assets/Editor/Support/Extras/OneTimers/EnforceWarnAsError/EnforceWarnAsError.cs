using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class EnforceWarnAsError
{
	static string mcsPath = Application.dataPath + "/mcs.rsp";
	static string optionTag = "-warnaserror";

	[MenuItem("Extras/One Timers/Enforce Warn As Error", false, 20)]
	static void Execute()
	{ 
		if(EditorUtility.DisplayDialog(
			"Enforce Warn As Error",
			"This will create a files that will treat warning as error.\n",
			"OK", "Cancel"
		)) {
			AddToFile();
		}
	}

	[MenuItem("Extras/One Timers/Enforce Warn As Error", true)]
	static bool Check()
	{
		return !File.Exists(mcsPath);
	}

	static void AddToFile()
	{
		string text = "";
		FileInfo fileInfo = new FileInfo(mcsPath);

		if(fileInfo.Exists) {
			text = ReadFile(fileInfo);
		} else {
			using(fileInfo.Create());
		}

		if(text.Contains(optionTag)) {
			Debug.LogFormat("{0} already contains {1}", mcsPath, optionTag);
		} else {
			WriteContents(fileInfo);
		}
	}

	static string ReadFile(FileInfo fileInfo)
	{
		using(var reader = fileInfo.OpenText()) {
			return reader.ReadToEnd();
		};
	}

	static void WriteContents(FileInfo fileInfo)
	{
		using(var writer = fileInfo.AppendText()) {
			writer.Write(optionTag + "+\n");
		}
	}
}
