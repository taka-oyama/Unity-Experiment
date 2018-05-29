using System;
using UnityEngine;
using Zenject;

public partial class Character : MonoBehaviour
{
	[Inject]
	public CharacterData Data;

	void Update()
	{
		UpdateName();
	}

	public void MarryWith(Character spouse)
	{
		Data.MarryWith(spouse.Data);
	}
}
