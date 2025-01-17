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

        [Inject]
        public void Constract(
            GameEntryPoint gameEntryPoint,
            IUIManager uiManager)
        {
            _uiManager = uiManager;
            _gameEntryPoint = gameEntryPoint;
        }

        public Observable<MainMenuExitParams> Run(MainMenuEnterParams sceneEnterParams)
        {
            _mainMenuEnterParams = sceneEnterParams;
           
            var gamePlayEnterParams = new GamePlayEnterParams(10);
            var exitParams = new MainMenuExitParams(gamePlayEnterParams);
            var exitToGamePlaySceneSignal = ExitMainMenu.Select(_ => exitParams);
 
            var ui = _uiManager.ShowUI<UIMainMenuRootController, UIMainMenuRootView>(UIPathPrefabs.UIMenuRoot, _gameEntryPoint.UIRootView.SceneContainer, true);
            _uiManager.ShowUI<UIMenuController, UIMenuView>(UIPathPrefabs.UIMenu, ui.UIScene);

            return exitToGamePlaySceneSignal;
        }
    }
}