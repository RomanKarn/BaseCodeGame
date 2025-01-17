using myGame.Code.Services.UIManagerController;
using UnityEngine;
using Zenject;

namespace myGame.Code.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    { 
        public override void InstallBindings()
        {
            Container.Bind<IUIManager>().To<UIManager>().AsSingle().NonLazy();
            Container.Bind<MainMenuEntryPoint>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Debug.Log("MainMenu install");
        }
    }
}