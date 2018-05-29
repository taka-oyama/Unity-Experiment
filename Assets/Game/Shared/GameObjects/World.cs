using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class World : MonoBehaviour
{
	[Inject]
	CharacterFactory characterFactory;

	[SerializeField]
	GameObject charactersPlaceholder;

	[SerializeField]
	int birthRatePercentage = 100;

	List<Character> population;

	void Awake()
	{
		population = new List<Character>();
		InvokeRepeating(nameof(EachSecond), 0f, (float)WorldData.YearInterval.TotalSeconds);
	}

	public void EachSecond()
	{
		List<Character> reproduceables = new List<Character>();
		List<Character> marriableMales = new List<Character>();
		List<Character> marriableFemales = new List<Character>();

		foreach(Character character in population) {
			CharacterData data = character.Data;
			if(data.CanReproduce) reproduceables.Add(character);
			if(data.IsMale && data.CanMarry) marriableMales.Add(character);
			if(data.IsFemale && data.CanMarry) marriableFemales.Add(character);
		}

		int matchableCount = Math.Min(marriableMales.Count, marriableFemales.Count);
		//matchableCount = (int)Mathf.Ceil(UnityEngine.Random.Range(0, matchableCount) / 2f);
		for(int i = 0; i < matchableCount; i++) {
			if(marriableMales.IsNotEmpty() && marriableFemales.IsNotEmpty()) {
				Character male = marriableMales.RemoveRandom();
				Character female = marriableFemales.RemoveRandom();
				male.MarryWith(female);
				Debug.LogFormat("結婚確定: {0} + {1}", male.Data.Name, female.Data.Name);
			}
		}

		foreach(Character reproduceable in reproduceables) {
			if(UnityEngine.Random.Range(0, 100) <= birthRatePercentage) {
				Character child = characterFactory.Create(reproduceable.Data.Impregenate());
				population.Add(child);
				charactersPlaceholder.Append(child);
				Debug.LogFormat("妊娠確定: {0} + {1} = {2}", child.Data.Father.Name, child.Data.Mother.Name, child.Data.Name);
			}
		}
	}

	public void OnClick()
	{
		Character character = characterFactory.CreateGeneric();
		population.Add(character);
		charactersPlaceholder.Append(character);
	}
}
