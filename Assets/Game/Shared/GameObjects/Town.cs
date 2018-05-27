using UnityEngine;
using Zenject;

public class Town : MonoBehaviour
{
	[Inject]
	CharacterFactory characterFactory;

	[SerializeField]
	GameObject charactersPlaceholder;

	public void OnClick()
	{
		Character character = characterFactory.CreateGeneric();
		charactersPlaceholder.Append(character);
	}
}
