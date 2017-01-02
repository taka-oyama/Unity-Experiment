using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleBuilder
{
	[MenuItem("File/AssetBundle/Build", true, 1)]
	static void Build()
	{
		BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
		string outputPath = string.Join("/", new [] { Application.dataPath, "AssetBundles", buildTarget.ToString() });
		BuildAssetBundleOptions options = BuildAssetBundleOptions.ChunkBasedCompression | BuildAssetBundleOptions.StrictMode;
		Directory.CreateDirectory(outputPath);
		BuildPipeline.BuildAssetBundles(outputPath, options, buildTarget);
	}
}
