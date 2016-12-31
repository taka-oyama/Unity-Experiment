using UnityEngine;

public class SingletonBehaviour<T> : GlobalBehaviour<T> where T : MonoBehaviour
{
	public override void Setup()
	{
		DontDestroyOnLoad(gameObject);
	}
}
