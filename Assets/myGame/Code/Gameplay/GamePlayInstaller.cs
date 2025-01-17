using myGame.Code.Gameplay.Services.ResourceService;
using myGame.Code.Gameplay.Services.WorldManagerService;
using myGame.Code.Gameplay.UIRoot;
using myGame.Code.Services.UIManagerController;
using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay
{
    public class GamePlayInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _uiRootGamePlay;
        public override void InstallBindings()
        {
            Container.Bind<IWorldManager>().To<WorldManager>().AsSingle().NonLazy();
            Container.Bind<IUIManager>().To<UIManager>().AsSingle().NonLazy();
            Container.Bind<IResourcesService>().To<ResourcesService>().AsSingle().NonLazy();
            Container.Bind<GamePlayEntryPoint>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Debug.Log("GamePlayInstaller install");
        }
    }
}
