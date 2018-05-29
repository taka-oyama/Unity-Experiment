using System;
using UnityEngine;

public class CharacterFactory : Zenject.Factory<CharacterData, Character>
{
	public Character CreateGeneric()
	{
		return Create(CharacterData.Create());
	}
}
