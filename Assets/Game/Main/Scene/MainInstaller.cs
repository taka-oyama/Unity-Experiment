using UnityEngine;

public class MainInstaller : SceneInstaller
{
	[SerializeField]
	Character characterPrefab;

    public override void InstallBindings()
    {
		base.InstallBindings();

		Container.BindFactory<CharacterData, Character, CharacterFactory>().FromComponentInNewPrefab(characterPrefab);
    }
}