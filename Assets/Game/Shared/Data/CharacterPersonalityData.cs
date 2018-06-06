using UnityEngine;
using Game;

public class CharacterPersonalityData
{
	public int Attention;   // extrovert <-> introvert
	public int Perception;  //   sensing <-> intuition
	public int Decision;    //  thinking <-> feeling
	public int Orientation; //   judging <-> preceiving

	const int MinValue = 0;
	const int MaxValue = 10;

	public static CharacterPersonalityData Create(
		int? attention = null,
		int? perception = null,
		int? decision = null,
		int? orientation = null)
	{
		return new CharacterPersonalityData {
			Attention = attention ?? new IntRange(MinValue, MaxValue).Sample(),
			Perception = perception ?? new IntRange(MinValue, MaxValue).Sample(),
			Decision = decision ?? new IntRange(MinValue, MaxValue).Sample(),
			Orientation = orientation ?? new IntRange(MinValue, MaxValue).Sample(),
		};
	}

	public CharacterPersonalityData Copy()
	{
		return new CharacterPersonalityData {
			Attention = Attention,
			Perception = Perception,
			Decision = Decision,
			Orientation = Orientation,
		};
	}

	public static CharacterPersonalityData operator +(CharacterPersonalityData p1, CharacterPersonalityData p2)
	{
		return new CharacterPersonalityData {
			Attention = p1.Attention + p2.Attention,
			Perception = p1.Perception + p2.Perception,
			Decision = p1.Decision + p2.Decision,
			Orientation = p1.Orientation + p2.Orientation,
		};
	}

	public static CharacterPersonalityData operator *(CharacterPersonalityData p, float multiplier)
	{
		return new CharacterPersonalityData {
			Attention = (int)(p.Attention * multiplier),
			Perception = (int)(p.Perception * multiplier),
			Decision = (int)(p.Decision * multiplier),
			Orientation = (int)(p.Orientation * multiplier),
		};
	}

	public static CharacterPersonalityData operator /(CharacterPersonalityData p, float divisor)
	{
		return new CharacterPersonalityData {
			Attention = (int)(p.Attention / divisor),
			Perception = (int)(p.Perception / divisor),
			Decision = (int)(p.Decision / divisor),
			Orientation = (int)(p.Orientation / divisor),
		};
	}
}
