using UnityEngine;
using System.Collections;

public abstract class Initializer<T> : SingletonBehaviour<T> where T : Initializer<T>
{
	void Start()
	{

	}

	public void AddGlobal<U>() where U : GlobalBehaviour<U>
	{
		GameObject go = new GameObject();
		go.transform.parent = transform;
		go.AddComponent<U>();
	}
}
