using UnityEngine;

public partial class Character : MonoBehaviour
{
	int cachedAge = int.MinValue;

	void UpdateName()
	{
		if(Data.Age != cachedAge) {
			name = Data.Name + " (" + Data.Age + "歳 " + (Data.Gender == Gender.Male ? "男" : "女") + ")";
			cachedAge = Data.Age;
		}
	}
}
