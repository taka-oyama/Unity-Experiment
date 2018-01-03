using UnityEngine;
using Zenject;
using Game.SceneManagement;
using Game.ErrorHandling;

public class ProjectInstaller : MonoInstaller
{
	public ErrorHandler errorHandlerPrefab;

    public override void InstallBindings()
    {
		Container.Bind<ErrorHandler>().FromComponentInNewPrefab(errorHandlerPrefab).AsSingle().NonLazy();
		Container.Bind<NotificationCenter>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		Container.Bind<SceneNavigator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
    }
}