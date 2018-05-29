using System;
using System.Collections.Generic;
using System.Linq;

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
	public List<CharacterData> Children;

	public bool IsMale => Gender == Gender.Male;
	public bool IsFemale => Gender == Gender.Female;

	public int Age => (int)Math.Floor(Birthday.SpanFromNow().TotalSeconds / 1.GameYears().TotalSeconds);

	public bool IsSingle => Spouse == null;
	public bool IsMarried => !IsSingle;
	public bool IsPregnant => Children != null && Children.Any(c => c.Age < 0);

	public bool CanMarry => IsSingle && Age >= 18;
	public bool CanReproduce => IsFemale && IsMarried && !IsPregnant;

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
		data.Birthday = birthday ?? UnityEngine.Random.Range(15f, 25f).GameYears().Ago();
		data.Personality = personality ?? CharacterPersonalityData.Create();
		data.BaseSpec = baseSpec ?? CharacterSpecData.Create();
		data.CompoundSpec = compoundSpec ?? new CharacterSpecData();
		data.Mother = mother;
		data.Father = father;
		data.Spouse = spouse;
		return data;
	}

	public void MarryWith(CharacterData spouce)
	{
		Spouse = spouce;
		spouce.Spouse = this;
	}

	public CharacterData Impregenate()
	{
		CharacterData childData = Create(
			birthday: DateTime.Now + UnityEngine.Random.Range(240, 320).GameDays(),
			father: IsMale ? this : Spouse,
			mother: IsFemale ? this : Spouse,
			personality: Personality + Spouse.Personality * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f,
			baseSpec: BaseSpec + Spouse.BaseSpec * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f
		);
		Children = Children ?? new List<CharacterData>();
		Children.Add(childData);
		return childData;
	}
}
