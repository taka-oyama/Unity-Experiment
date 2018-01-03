using System;
using UnityEngine;

namespace Game
{
	public abstract class GlobalBehaviour<T> : MonoBehaviour where T : GlobalBehaviour<T>
	{
		public static T Instance { get; protected set; }
		protected static bool Exists { get { return Instance != null; } }
		protected static bool NotExists { get { return !Exists; } }

		protected virtual void Awake()
		{
			if (Exists) {
				string message = string.Format("Singleton Instance <{0}> already exists!", GetType().Name);
				throw new Exception(message);
			}
			Instance = this as T;
			this.name = string.Concat(GetType().Name, " (Global)");
		}

		protected virtual void OnDestroy()
		{
			Instance = null;
		}
	}
}
