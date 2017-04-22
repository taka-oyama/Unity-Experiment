using UnityEngine;

public class Test : MonoBehaviour
{
	ExpMeter calc;

	void Start()
	{
		calc = GetComponent<ExpMeter>().Setup(new int[1], 0);
		calc.Add(300);
	}
}
