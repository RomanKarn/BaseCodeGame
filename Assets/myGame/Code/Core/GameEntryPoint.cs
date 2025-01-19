using System.Collections;
using myGame.Code.Core.UIView;
using myGame.Code.Gameplay;
using myGame.Code.MainMenu;
using myGame.Code.Services.Analytic;
using myGame.Code.Services.AudioController;
using myGame.Code.Services.CoroutineController;
using myGame.Code.Services.LanguageLocalaze;
using myGame.Code.Services.SDKYandex;
using myGame.Code.Settings;
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
        private ISettingsProvider _settingsProvider;
        private ILanguageLocalazeServices _localaze;
        private IAudioService _audioService;
        private IAnalyticsService _analyticsService;
        private ISDK _sdk;

        [Inject]
        public void Constract(
            ICoroutineService coroutines, 
            IGameStateProvider stateProvider,
            ISettingsProvider settingsProvider,
            ILanguageLocalazeServices localaze,
            IAudioService audioService,
            IAnalyticsService analyticsService,
            ISDK sdk)
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            
            _coroutines = coroutines;
            _gameStateProvider = stateProvider;
            _settingsProvider = settingsProvider;
            _localaze = localaze;
            _audioService = audioService;
            _analyticsService = analyticsService;
            _sdk = sdk;
            
            _gameStateProvider.LoadSettingsState(_settingsProvider.ApplicationSettings);
            _gameStateProvider.LoadGameState();
            _audioService.Initialize();
            _analyticsService.Initialize(5456464);
            _sdk.Initialize();
            
            var prefabUIRoot = Resources.Load<UIRootView>("UI/UIRootCanvas");
            _uiRootView = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRootView.gameObject);
            
            _uiRootView.ShowLoadingScreen();
            _coroutines.StartTrackedCoroutine(InitAsing());
        }

        private IEnumerator InitAsing()
        {
            yield return _sdk.InitializeAsync();
            RunGame();
        }
        
        private void RunGame()
        {
            _analyticsService.TrackGoal(YandexAnalyticsEventName.INIT);
            
    #if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;
            
            if (sceneName == Scenes.GAMEPLAY)
            {
                _gameStateProvider.ResetGameState();
                _gameStateProvider.ResetSettingsState(_settingsProvider.ApplicationSettings);
                var gamePlayEnterParams = new GamePlayEnterParams();
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
            Resources.UnloadUnusedAssets();
            yield return LoadScene(Scenes.GAMEPLAY);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<GamePlayEntryPoint>();
            sceneEntryPoint.Run(gamePlayEnterParams).Subscribe(gamePlayExitParams =>
            {
                _coroutines.StopAllTrackedCoroutines();
                var targetSceneName = gamePlayExitParams.TargetSceneEnterParams.SceneName;
                if (targetSceneName == Scenes.MAINMENU)
                {
                    _coroutines.StartTrackedCoroutine(LoadAndStartMaimMenu(gamePlayExitParams.TargetSceneEnterParams.As<MainMenuEnterParams>()));
                }
            });;
            yield return new WaitForSeconds(0.5f);

            _localaze.LoadLanguage();
            _uiRootView.HideLoadingScreen();
        }
        private IEnumerator LoadAndStartMaimMenu(MainMenuEnterParams mainMenuEnterParams = null)
        {
            _uiRootView.ShowLoadingScreen();
            yield return LoadScene(Scenes.BOOT);
            Resources.UnloadUnusedAssets();
            yield return LoadScene(Scenes.MAINMENU);
            
            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();
            yield return new WaitForSeconds(1);
            sceneEntryPoint.Run(mainMenuEnterParams).Subscribe(mainMenuExitParams =>
            {
                _coroutines.StopAllTrackedCoroutines();
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;
                if (targetSceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartTrackedCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GamePlayEnterParams>()));
                }
            });
            
            yield return new WaitForSeconds(0.5f);

            _localaze.LoadLanguage();
            _uiRootView.HideLoadingScreen();
        }
        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }

    }
}

