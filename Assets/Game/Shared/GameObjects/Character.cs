using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public partial class Character : MonoBehaviour
{
	[Inject]
	public CharacterData Data;

	public Guid Id => Data.Id;
	public string Name => Data.Name;
	public Gender Gender => Data.Gender;
	public DateTime Birthday => Data.Birthday;
	public DateTime Deathday => Data.Deathday;

	public Character Father { get; protected set; }
	public Character Mother { get; protected set; }
	public Character Spouse { get; protected set; }
	public List<Character> Children = new List<Character>();

	public int Age => (cachedAge = cachedAge ?? (int)CalculateAge()).Value;
	public bool IsMale => Gender == Gender.Male;
	public bool IsFemale => Gender == Gender.Female;
	public bool IsSingle => Spouse == null;
	public bool IsWorkable => Age >= 15;
	public bool IsRetirable => Age >= 70;
	public bool CanMarry => IsSingle && Age >= 18;
	public bool IsMarried => !IsSingle;
	public bool CanReproduce => IsFemale && IsMarried && !IsPregnant;
	public bool IsPregnant => Children.Any(c => c.Age < 0);
	public bool IsDead => DateTime.Now <= Deathday;

	int? cachedAge;

	void Awake()
	{
		UpdateName();
	}

	void Update()
	{
		UpdateName();
	}

	void LateUpdate()
	{
		cachedAge = null;
	}

	double CalculateAge()
	{
		return Math.Floor(Birthday.SpanFromNow().TotalSeconds / 1.GameYears().TotalSeconds);
	}

	public void Die()
	{
		Destroy(gameObject);
	}

	public bool CanMarryWith(Character spouse)
	{
		if(!CanMarry) return false;
		if(!spouse.CanMarry) return false;

		if(Mother == null || Father == null) return true;
		if(spouse.Mother == null || spouse.Father == null) return true;

		if(Mother?.Id == spouse.Mother?.Id) return false;
		if(Father?.Id == spouse.Father?.Id) return false;

		return true;
	}

	public void MarryWith(Character target)
	{
		Spouse = target;
		Spouse.Data = target.Data;

		target.Spouse = this;
		target.Spouse.Data = this.Data;
	}

	public Character Impregenate(CharacterFactory factory)
	{
		CharacterData childData = CharacterData.Create(
			birthday: DateTime.Now + UnityEngine.Random.Range(240, 320).GameDays(),
			fatherId: IsMale ? Id : Spouse.Id,
			motherId: IsFemale ? Id : Spouse.Id,
			personality: Data.Personality + Spouse.Data.Personality * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f,
			baseSpec: Data.BaseSpec + Spouse.Data.BaseSpec * UnityEngine.Random.Range(-1.25f, 1.25f) / 2f
		);
		Data.ChildrenIds.Add(childData.Id);

		Character child = factory.Create(childData);
		child.Mother = this;
		child.Father = child.Mother.Spouse;
		Children.Add(child);

		return child;
	}
}
