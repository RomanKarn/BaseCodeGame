using myGame.Code.MainMenu.UIRoot;
using myGame.Code.MainMenu.UIRoot.UIMenu;
using myGame.Code.Services.UIManagerController;
using UnityEngine;
using Zenject;

namespace myGame.Code.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _uiRootMainMenu;
        [SerializeField] private GameObject _uiMenu;
        public override void InstallBindings()
        {
            Container.Bind<MainMenuEntryPoint>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            
            Container.Bind<GameObject>().WithId("RootMenu").FromInstance(_uiRootMainMenu);
            Container.Bind<GameObject>().WithId("Menu").FromInstance(_uiMenu);

            // Регистрируем контроллеры UI как Transient, чтобы создавать их заново при каждом вызове
            Container.Bind<UIMainMenuRootController>().AsTransient();
            Container.Bind<UIMenuController>().AsTransient();
            
            Debug.Log("MainMenu install");
        }
    }
}