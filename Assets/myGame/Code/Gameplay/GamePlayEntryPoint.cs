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
        public Subject<Unit> ExitGamePlay = new Subject<Unit>();
        
        private GameEntryPoint _gameEntryPoint;
        private IUIManager _uiManager;
        private GamePlayEnterParams _gamePlayEnterParams;

        [Inject]
        public void Constract(
            GameEntryPoint gameEntryPoint,
            IUIManager uiManager)
        {
            _uiManager = uiManager;
            _gameEntryPoint = gameEntryPoint;

        }
        
        public Observable<GamePlayExitParams> Run(GamePlayEnterParams sceneEnterParams)
        {
            _gamePlayEnterParams = sceneEnterParams;
            
            var exitSceneSignalSubj = new Subject<Unit>();
            
            var mainMenuEnterParams = new MainMenuEnterParams(10);
            
            var exitParams = new GamePlayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = ExitGamePlay.Select(_ => exitParams);
            
            _uiManager.ShowUI<UIGamePlayRootController, UIGamePlayRootView>(UIPathPrefabs.UIGamePlayRoot, _gameEntryPoint.UIRootView.SceneContainer, true);
            
            return exitToMainMenuSceneSignal;
        }
    }
}
