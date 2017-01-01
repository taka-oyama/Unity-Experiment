using UnityEngine;

public class SingletonBehaviour<T> : GlobalBehaviour<T> where T : SingletonBehaviour<T>
{
	public override void Setup()
	{
		DontDestroyOnLoad(gameObject);
	}
}
