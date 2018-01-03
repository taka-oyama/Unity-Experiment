using UnityEngine;

namespace Game
{
	public class SingletonBehaviour<T> : GlobalBehaviour<T> where T : SingletonBehaviour<T>
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(gameObject);
			this.name = string.Concat(typeof(T).Name, " (Singleton)");
		}
	}
}
