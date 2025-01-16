using myGame.Code.Services.CoroutineController;
using myGame.Code.Services.UIManagerController;
using myGame.Code.State;
using UnityEngine;
using Zenject;

namespace myGame.Code.Core
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            
            var coroutineServiceGameObject = new GameObject("[COROUTINE_SERVICE]");
            var coroutineService = coroutineServiceGameObject.AddComponent<CoroutinesService>();
            DontDestroyOnLoad(coroutineServiceGameObject);
            
            Container.Bind<IUIManager>().To<UIManager>().AsSingle().NonLazy();
            Container.Bind<ICoroutineService>().FromInstance(coroutineService).AsSingle();
            Container.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().AsSingle();
            
            Container.Bind<GameEntryPoint>().AsSingle().NonLazy();
            
            Debug.Log("GlobalInstaller install");
        }
    }
}
