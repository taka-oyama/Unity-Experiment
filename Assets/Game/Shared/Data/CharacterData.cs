using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterData
{
	public Guid Id;
	public string Name;
	public Gender Gender;
	public DateTime Birthday;
	public DateTime Deathday;

	public CharacterPersonalityData Personality;
	public CharacterSpecData BaseSpec;
	public CharacterSpecData CompoundSpec;

	public Guid? MotherId;
	public Guid? FatherId;
	public Guid? SpouseId;
	public List<Guid> ChildrenIds;

	public CharacterData Copy()
	{
		return new CharacterData {
			Id = Id,
			Name = Name,
			Gender = Gender,
			Birthday = Birthday,
			Deathday = Deathday,
			Personality = Personality.Copy(),
			BaseSpec = BaseSpec.Copy(),
			CompoundSpec = CompoundSpec.Copy(),
			MotherId = MotherId,
			FatherId = FatherId,
			SpouseId = SpouseId,
			ChildrenIds = ChildrenIds.Select(guid => guid).ToList(),
		};
	}

	public static CharacterData Create(
		Guid? id = null,
		Gender? gender = null,
		string name = null,
		DateTime? birthday = null,
		CharacterPersonalityData personality = null,
		CharacterSpecData baseSpec = null,
		CharacterSpecData compoundSpec = null,
		Guid? motherId = null,
		Guid? fatherId = null,
		Guid? spouseId = null,
		List<Guid> childrenIds = null)
	{
		CharacterData data = new CharacterData();
		data.Id = id ?? Guid.NewGuid();
		data.Gender = gender ?? CharacterGenderPicker.Pick();
		data.Name = name ?? CharacterNamePicker.Pick(data.Gender);
		data.Birthday = birthday ?? UnityEngine.Random.Range(15f, 25f).GameYears().Ago();
		data.Deathday = data.Birthday + UnityEngine.Random.Range(60f, 100f).GameYears();
		data.Personality = personality ?? CharacterPersonalityData.Create();
		data.BaseSpec = baseSpec ?? CharacterSpecData.Create();
		data.CompoundSpec = compoundSpec ?? new CharacterSpecData();
		data.MotherId = motherId;
		data.FatherId = fatherId;
		data.SpouseId = spouseId;
		data.ChildrenIds = childrenIds ?? new List<Guid>();
		return data;
	}
}
