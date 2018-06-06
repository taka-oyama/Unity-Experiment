using UnityEngine;

public partial class Character : MonoBehaviour
{
	void UpdateName()
	{
		string ageString = Age > 0 ? Age.ToString() : "？";
		string gender = (Data.Gender == Gender.Male ? "男" : "女");
		name = Data.Name + " (" + ageString + "歳 " + gender + ")";
	}
}
