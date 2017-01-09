using UnityEngine;

public class GameInitializer : SingletonBehaviour<GameInitializer>
{
	protected override void Awake()
	{
		base.Awake();
		InitSingleton<SceneNavigator>();
	}

	public static GameInitializer CreateIfNotExist()
	{
		return Exists ? I : InitSingleton<GameInitializer>();
	}

	public static T InitSingleton<T>() where T : SingletonBehaviour<T>
	{
		return new GameObject().AddComponent<T>();
	}
}
