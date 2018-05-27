using System;
using Game;
using UnityEngine;

[Serializable]
public class CharacterData
{
	public Guid Id;
	public string Name;
	public Gender Gender;
	public DateTime Birthday;

	public CharacterPersonalityData Personality;
	public CharacterSpecData BaseSpec;
	public CharacterSpecData CompoundSpec;

	public CharacterData Mother;
	public CharacterData Father;
	public CharacterData Spouse;

	public bool IsMale => Gender == Gender.Male;
	public bool IsFemale => Gender == Gender.Female;

	public int Age => (int)(Birthday.SpanFromNow().TotalDays / WorldData.DayInterval.TotalDays);

	public static CharacterData Create(
		Guid? id = null,
		Gender? gender = null,
		string name = null,
		DateTime? birthday = null,
		CharacterPersonalityData personality = null,
		CharacterSpecData baseSpec = null,
		CharacterSpecData compoundSpec = null,
		CharacterData mother = null,
		CharacterData father = null,
		CharacterData spouse = null)
	{
		CharacterData data = new CharacterData();
		data.Id = id ?? Guid.NewGuid();
		data.Gender = gender ?? CharacterGenderPicker.Pick();
		data.Name = name ?? CharacterNamePicker.Pick(data.Gender);
		data.Birthday = birthday ?? DateTime.Now - UnityEngine.Random.Range(15f, 25f).GameDays();
		data.Personality = personality ?? CharacterPersonalityData.Create();
		data.BaseSpec = baseSpec ?? CharacterSpecData.Create();
		data.CompoundSpec = compoundSpec ?? new CharacterSpecData();
		data.Mother = mother;
		data.Father = father;
		data.Spouse = spouse;
		return data;
	}

	public float AgeMultiplier
	{
		get
		{
			int age = Age;
			if(age.IsBetween(10, 19)) return 1.0f;
			if(age.IsBetween(20, 29)) return 1.2f;
			if(age.IsBetween(30, 39)) return 1.0f;
			if(age.IsBetween(40, 49)) return 0.7f;
			if(age.IsBetween(50, 59)) return 0.5f;
			return 0f;
		}
	}

	public void MarryWith(CharacterData spouce)
	{
		Spouse = spouce;
		spouce.Spouse = this;
	}

	public CharacterData Impregenate(CharacterData spouse)
	{
		return Create(
			birthday: DateTime.Now + UnityEngine.Random.Range(240, 320).GameDays(),
			father: IsMale ? this : spouse,
			mother: IsFemale ? this : spouse,
			personality: Personality + spouse.Personality * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f,
			baseSpec: BaseSpec + spouse.BaseSpec * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f
		);
	}
}
