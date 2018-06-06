using UnityEngine;

public static class PregnancyCalculator
{
	public static float GetPercentage(Character female)
	{
		return GetMultiplier(female) * GetMultiplier(female.Spouse) * 100f;
	}

	static float GetMultiplier(Character character)
	{
		int age = character.Age;
		if(age.IsBetween(16, 20)) return 0.10f;
		if(age.IsBetween(21, 30)) return 0.60f;
		if(age.IsBetween(31, 40)) return 0.50f;
		if(age.IsBetween(41, 50)) return 0.30f;
		return 0f;
	}
}
