using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
	ExpMeter calc;

	void Start()
	{
		
	}

	public void OnClick()
	{
		throw new UnityException("test");
	}
}
