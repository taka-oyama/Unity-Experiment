using System;
using UnityEngine;

public abstract class GlobalBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T I { get; protected set; }
	public static bool Exists { get { return I != null; } }
	public static bool NotExists { get { return !Exists; } }

	public abstract void Setup();

	void Awake()
	{
		if (Exists) {
			string message = string.Format("Singleton Instance <{0}> already exists!", typeof(T).Name);
			throw new Exception(message);
		}
		GameObject go = new GameObject();
		go.name = typeof(T).ToString() + " (Singleton)";
		I = go.AddComponent<T>();
		Setup();
	}

	public T Reset()
	{
		gameObject.RemoveComponent<T>();
		return gameObject.AddComponent<T>();
	}

	void OnDestroy()
	{
		I = null;
	}
}
