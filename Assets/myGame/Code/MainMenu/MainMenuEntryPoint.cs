using myGame.Code.Core;
using myGame.Code.Gameplay;
using myGame.Code.MainMenu.UIRoot;
using myGame.Code.MainMenu.UIRoot.UIMenu;
using myGame.Code.Services.UIManagerController;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        public Subject<Unit> ExitMainMenu = new Subject<Unit>();
        
        private GameEntryPoint _gameEntryPoint;
        private IUIManager _uiManager;
        private MainMenuEnterParams _mainMenuEnterParams;
        private GameObject _rootMenuPrefab;
        private GameObject _menuPrefab;

        [Inject]
        public void Constract(
            GameEntryPoint gameEntryPoint,
            IUIManager uiManager,
            [Inject(Id = "RootMenu")] GameObject rootMenuPrefab,
            [Inject(Id = "Menu")] GameObject menuPrefab)
        {
            _uiManager = uiManager;
            _gameEntryPoint = gameEntryPoint;
            _rootMenuPrefab = rootMenuPrefab;
            _menuPrefab = menuPrefab;
        }

        public Observable<MainMenuExitParams> Run(MainMenuEnterParams sceneEnterParams)
        {
            _mainMenuEnterParams = sceneEnterParams;
           
            var gamePlayEnterParams = new GamePlayEnterParams(10);
            var exitParams = new MainMenuExitParams(gamePlayEnterParams);
            var exitToGamePlaySceneSignal = ExitMainMenu.Select(_ => exitParams);
 
            var ui = _uiManager.ShowUI<UIMainMenuRootController, UIMainMenuRootView>(_rootMenuPrefab, _gameEntryPoint.UIRootView.SceneContainer);
            _uiManager.ShowUI<UIMenuController, UIMenuView>(_menuPrefab, ui.UIScene);

            return exitToGamePlaySceneSignal;
        }
    }
}