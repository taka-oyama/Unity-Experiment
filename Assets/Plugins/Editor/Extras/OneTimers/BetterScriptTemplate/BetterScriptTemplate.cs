using UnityEngine;
using UnityEditor;
using System.IO;

public class BetterScriptTemplate
{
	static string myTemplatePath = Application.dataPath + "/Plugins/Editor/Extras/OneTimers/BetterScriptTemplate";
	static string appTemplatePath = string.Join("/", new [] { EditorApplication.applicationPath, "Contents", "Resources", "ScriptTemplates" });

	[MenuItem("Extras/One Timers/Better C# Template", false, 100)]
	static void Execute()
	{
		if(EditorUtility.DisplayDialog(
			   "Simplify C# Template",
			   "This will replace C# template with a simpler one and delete the Javascript template.\n\nThis requires you to restart Unity.\n",
			   "OK", "Cancel"
		   )) {
			MoveFiles();
		}
	}

	[MenuItem("Extras/One Timers/Better C# Template", true)]
	static bool Check()
	{
		return !File.Exists(appTemplatePath + "/81-C# Script-NewBehaviourScript.cs.txt.bak");
	}

	static void MoveFiles()
	{
		string csTemplatePath = appTemplatePath + "/81-C# Script-NewBehaviourScript.cs.txt";
		string jsTemplatePath = appTemplatePath + "/82-Javascript-NewBehaviourScript.js.txt";
		string newCsTemplatePath = myTemplatePath + "/81-C# Script-NewBehaviourScript.cs.txt";

		if(!File.Exists(csTemplatePath + ".bak")) {
			File.Move(csTemplatePath, csTemplatePath + ".bak");
		} else {
			Debug.LogWarningFormat("{0}.bak already exists", csTemplatePath);
		}

		if(!File.Exists(jsTemplatePath + ".bak")) {
			File.Move(jsTemplatePath, jsTemplatePath + ".bak");
		} else {
			Debug.LogWarningFormat("{0}.bak already exists", jsTemplatePath);
		}

		File.Copy(newCsTemplatePath, csTemplatePath, true);
	}
}
