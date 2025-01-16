using myGame.Code.Core;
using myGame.Code.Gameplay.UIRoot;
using myGame.Code.MainMenu;
using myGame.Code.Services.UIManagerController;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay
{
    public class GamePlayEntryPoint : MonoBehaviour
    {
        private GameEntryPoint _gameEntryPoint;
        private IUIManager _uiManager;
        private GamePlayEnterParams _gamePlayEnterParams;
        private GameObject _menuPrefab;

        [Inject]
        public void Constract(
            GameEntryPoint gameEntryPoint,
            IUIManager uiManager,
            [Inject(Id = "RootGamePlay")] GameObject menuPrefab)
        {
            _uiManager = uiManager;
            _gameEntryPoint = gameEntryPoint;

            _menuPrefab = menuPrefab;
        }
        
        public Observable<GamePlayExitParams> Run(GamePlayEnterParams sceneEnterParams)
        {
            _gamePlayEnterParams = sceneEnterParams;
            
            var exitSceneSignalSubj = new Subject<Unit>();
            
            var mainMenuEnterParams = new MainMenuEnterParams(10);
            
            var exitParams = new GamePlayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);
            
            _uiManager.ShowUI<UIGamePlayRootController, UIGamePlayRootView>(_menuPrefab, _gameEntryPoint.UIRootView.SceneContainer);
            
            return exitToMainMenuSceneSignal;
        }
    }
}
