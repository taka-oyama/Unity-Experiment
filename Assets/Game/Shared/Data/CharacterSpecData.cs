using Game;
using UnityEngine;

public class CharacterSpecData
{
	public int Dexterity;
	public int Intelligence;
	public int Vitality;

	public const int MinValue = 0;
	public const int MaxValue = 9999;

	static int MinGenricValue = MinValue / 2;
	static int MaxGenricValue = MaxValue / 2;

	public static CharacterSpecData Create(
		int? dexterity = null,
		int? intelligence = null,
		int? vitality = null)
	{
		return new CharacterSpecData {
			Dexterity = dexterity ?? new IntRange(MinGenricValue, MaxGenricValue).Sample(),
			Intelligence = intelligence ?? new IntRange(MinGenricValue, MaxGenricValue).Sample(),
			Vitality = vitality ?? new IntRange(MinGenricValue, MaxGenricValue).Sample(),
		};
	}

	public CharacterSpecData Copy()
	{
		return new CharacterSpecData() {
			Dexterity = Dexterity,
			Intelligence = Intelligence,
			Vitality = Vitality,
		};
	}

	public static CharacterSpecData operator +(CharacterSpecData c1, CharacterSpecData c2)
	{
		return new CharacterSpecData() {
			Dexterity = c1.Dexterity + c2.Dexterity,
			Intelligence = c1.Intelligence + c2.Intelligence,
			Vitality = c1.Vitality + c2.Vitality,
		};
	}

	public static CharacterSpecData operator *(CharacterSpecData spec, float multiplier)
	{
		return new CharacterSpecData() {
			Dexterity = (int)(spec.Dexterity * multiplier),
			Intelligence = (int)(spec.Intelligence * multiplier),
			Vitality = (int)(spec.Vitality * multiplier),
		};
	}

	public static CharacterSpecData operator /(CharacterSpecData spec, float divisor)
	{
		return new CharacterSpecData() {
			Dexterity = (int)(spec.Dexterity / divisor),
			Intelligence = (int)(spec.Intelligence / divisor),
			Vitality = (int)(spec.Vitality / divisor),
		};
	}
}
