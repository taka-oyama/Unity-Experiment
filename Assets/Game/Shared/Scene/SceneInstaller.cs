using UnityEngine;
using Zenject;
using Game.SceneManagement;

public class SceneInstaller : MonoInstaller<SceneInstaller>
{
	[SerializeField]
	SceneBase scene;

    public override void InstallBindings()
    {
		Container.BindInstance(scene);
    }
}
