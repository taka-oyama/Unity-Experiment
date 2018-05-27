using System.Collections.Generic;
using UnityEngine;

public class TownData
{
	public int Electricity = 0;

	public int Food = 0;

	public int Cotton = 0;
	public int Wool = 0;
	public int Leather = 0;

	public int Wood = 0;
	public int Stone = 0;
	public int Copper = 0;
	public int Iron = 0;
	public int Steel = 0;
	public int Aluminum = 0;

	public List<Building> Buildings;
	public List<CharacterData> Citizens;

	public TownData()
	{
		Buildings = new List<Building>();
		Citizens = new List<CharacterData>();
	}
}
