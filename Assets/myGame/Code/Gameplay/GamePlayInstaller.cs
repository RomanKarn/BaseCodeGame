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
            
            Container.Bind<GameObject>().WithId("RootGamePlay").FromInstance(_uiRootGamePlay);
            Container.Bind<UIGamePlayRootController>().AsTransient();
            
        
            Container.Bind<GamePlayEntryPoint>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Debug.Log("GamePlayInstaller install");
        }
    }
}
