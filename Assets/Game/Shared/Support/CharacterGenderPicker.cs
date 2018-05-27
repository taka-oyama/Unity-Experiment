using UnityEngine;

public static class CharacterGenderPicker
{
	static readonly Gender[] genders = new Gender[] { Gender.Female, Gender.Male };

	public static Gender Pick()
	{
		return genders.Sample();
	}
}
