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

	List<Character> population;

	public static TimeSpan YearInterval = TimeSpan.FromSeconds(1);

	void Awake()
	{
		population = new List<Character>();
		InvokeRepeating(nameof(EachSecond), 0f, (float)YearInterval.TotalSeconds);
	}

	public void EachSecond()
	{
		List<Character> reproduceables = new List<Character>();
		List<Character> marriableMales = new List<Character>();
		List<Character> marriableFemales = new List<Character>();
		List<Character> willBeDeads = new List<Character>();

		foreach(Character character in population) {
			if(character.CanReproduce) reproduceables.Add(character);
			if(character.IsMale && character.CanMarry) marriableMales.Add(character);
			if(character.IsFemale && character.CanMarry) marriableFemales.Add(character);
			if(character.Age >= 85) willBeDeads.Add(character);
		}

		int matchableCount = Mathf.Min(marriableMales.Count, marriableFemales.Count);
		matchableCount = (int)Mathf.Ceil(UnityEngine.Random.Range(0f, matchableCount) / 2f);
		for(int i = 0; i < matchableCount; i++) {
			Character male = marriableMales.RemoveRandom();
			Character female = marriableFemales.RemoveRandom();
			if(male == null || female == null) {
				break;
			}
			if(male.CanMarryWith(female)) {
				male.MarryWith(female);
				Debug.LogFormat("結婚確定: {0} + {1}", male.name, female.name);
			}
		}

		foreach(Character reproduceable in reproduceables) {
			float percentage = PregnancyCalculator.GetPercentage(reproduceable);
			if(UnityEngine.Random.Range(0f, 100f) <= percentage) {
				Character child = reproduceable.Impregenate(characterFactory);
				population.Add(child);
				charactersPlaceholder.Append(child);
				Debug.LogFormat("妊娠確定: {0} + {1} = {2} ({3}%)", child.Father.name, child.Mother.name, child.name, percentage);
			}
		}

		foreach(Character willbeDead in willBeDeads) {
			if(willbeDead.Age > 85) {
				willbeDead.Die();
				population.Remove(willbeDead);
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
