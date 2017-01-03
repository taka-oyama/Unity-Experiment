using UnityEngine;

public class SingletonBehaviour<T> : GlobalBehaviour<T> where T : SingletonBehaviour<T>
{
	protected override void Awake()
	{
		base.Awake();
		DontDestroyOnLoad(gameObject);
	}
}
