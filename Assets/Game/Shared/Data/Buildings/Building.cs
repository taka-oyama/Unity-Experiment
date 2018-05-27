using System.Collections.Generic;

public abstract class Building
{
	public List<CharacterData> Members;

	protected Building(int capacity)
	{
		Members = new List<CharacterData>(capacity);
	}

	protected void Expand(int capacity)
	{
		Members.Capacity = capacity;
	}

	public abstract void Upgrade();
}
