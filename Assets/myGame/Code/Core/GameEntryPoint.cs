using System.Collections;
using myGame.Code.Core.UIView;
using myGame.Code.Gameplay;
using myGame.Code.MainMenu;
using myGame.Code.Services.CoroutineController;
using myGame.Code.State;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using R3;

namespace myGame.Code.Core
{
    public class GameEntryPoint
    { 
        public UIRootView UIRootView => _uiRootView;
        private UIRootView _uiRootView;
        private ICoroutineService _coroutines;
        private IGameStateProvider _gameStateProvider;
        [Inject]
        public void Constract(ICoroutineService coroutines, IGameStateProvider stateProvider)
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            _coroutines = coroutines;
            _gameStateProvider = stateProvider;

            _gameStateProvider.LoadSettingsState();
            _gameStateProvider.LoadGameState();
            
            var prefabUIRoot = Resources.Load<UIRootView>("UI/UIRootCanvas");
            _uiRootView = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);
            
            RunGame();
        }
        private void RunGame()
        {
    #if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                var gamePlayEnterParams = new GamePlayEnterParams(1);
                _coroutines.StartTrackedCoroutine(LoadAndStartGameplay(gamePlayEnterParams));
                return;
            }
            if (sceneName == Scenes.MAINMENU)
            {
                _coroutines.StartTrackedCoroutine(LoadAndStartMaimMenu());
                return;
            }
            if (sceneName == Scenes.BOOTSTRAP)
            {
                _coroutines.StartTrackedCoroutine(LoadAndStartMaimMenu());
                return;
            }
            if (sceneName != Scenes.BOOT)
            {
                return;
            }
    #endif

        _coroutines.StartTrackedCoroutine(LoadAndStartMaimMenu());
    }

        private IEnumerator LoadAndStartGameplay(GamePlayEnterParams gamePlayEnterParams)
        {
            _uiRootView.ShowLoadingScreen();
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.GAMEPLAY);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GamePlayEntryPoint>();
            sceneEntryPoint.Run(gamePlayEnterParams).Subscribe(gamePlayExitParams =>
            {
                var targetSceneName = gamePlayExitParams.TargetSceneEnterParams.SceneName;
                if (targetSceneName == Scenes.MAINMENU)
                {
                    _coroutines.StartTrackedCoroutine(LoadAndStartMaimMenu(gamePlayExitParams.TargetSceneEnterParams.As<MainMenuEnterParams>()));
                }
            });;
            yield return new WaitForSeconds(0.5f);

            _uiRootView.HideLoadingScreen();
        }
        private IEnumerator LoadAndStartMaimMenu(MainMenuEnterParams mainMenuEnterParams = null)
        {
            _uiRootView.ShowLoadingScreen();
            yield return LoadScene(Scenes.BOOT);
            yield return LoadScene(Scenes.MAINMENU);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            
            sceneEntryPoint.Run(mainMenuEnterParams).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;
                if (targetSceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartTrackedCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GamePlayEnterParams>()));
                }
            });
            
            yield return new WaitForSeconds(0.5f);

            _uiRootView.HideLoadingScreen();
        }
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

    }
}

