using UnityEngine;
using Zenject;

public class Character : MonoBehaviour
{
	public string Name;
	public Gender Gender;
	public int Age;

	[Inject]
	public CharacterData Data;

	void Awake()
	{
		this.name = Data.Name + " (" + Data.Gender + ")";

		this.Name = Data.Name;
		this.Gender = Data.Gender;
		this.Age = Data.Age;
	}

	void FixedUpdate()
	{
		this.Age = Data.Age;
	}
}
