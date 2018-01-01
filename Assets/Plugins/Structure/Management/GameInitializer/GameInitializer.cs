using UnityEngine;
using UnityEngine.Assertions;

public sealed class GameInitializer : SingletonBehaviour<GameInitializer>
{
	[SerializeField] GameObject[] preliminaries;

	protected override void Awake()
	{
		base.Awake();
		foreach(GameObject preliminary in preliminaries) {
			Instantiate(preliminary, transform.parent);
		}
	}

	public static void CreateIfNotExist()
	{
		if(NotExists) {
			string resourcePath = typeof(GameInitializer).Name;
			Object initializer = Instantiate(Resources.Load(resourcePath));

			Assert.IsNotNull(initializer, "Could not load " + resourcePath);
		}
	}

	void OnValidate()
	{
		this.preliminaries = ArrayValidator.RemoveDuplicateAndSort(preliminaries);
	}
}
