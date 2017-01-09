using System;
using UnityEngine;

public abstract class GlobalBehaviour<T> : MonoBehaviour where T : GlobalBehaviour<T>
{
	public static T I { get; protected set; }
	public static bool Exists { get { return I != null; } }
	public static bool NotExists { get { return !Exists; } }

	protected virtual void Awake()
	{
		if (Exists) {
			string message = string.Format("Singleton Instance <{0}> already exists!", typeof(T).Name);
			throw new Exception(message);
		}
		I = this as T;
		this.name = string.Concat(typeof(T).Name, " (Global)");
	}

	protected virtual void OnDestroy()
	{
		I = null;
	}
}
