using myGame.Code.Services.Analytic;
using myGame.Code.Services.AudioController;
using myGame.Code.Services.CoroutineController;
using myGame.Code.Services.GamePlayAndStop;
using myGame.Code.Services.LanguageLocalaze;
using myGame.Code.Services.SDKYandex;
using myGame.Code.Settings;
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
            
            Container.Bind<ICoroutineService>().FromInstance(coroutineService).AsSingle();
            Container.Bind<ISettingsProvider>().To<SettingsProvider>().AsSingle();
            Container.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().AsSingle();
            Container.Bind<IGamePlayAndStopSerice>().To<GamePlayAndStopSerice>().AsSingle();
            Container.Bind<ILanguageLocalazeServices>().To<LanguageLocalazeServices>().AsSingle();
            Container.Bind<IAudioService>().To<AudioService>().FromNewComponentOnNewGameObject().AsSingle();
            
            Container.Bind<IAnalyticsService>().To<YandexAnalytics>().AsSingle();
            Container.Bind<ISDK>().To<YandexSdk>().FromNewComponentOnNewGameObject().AsSingle();
            
            Container.Bind<GameEntryPoint>().AsSingle().NonLazy();
            
            Debug.Log("GlobalInstaller install");
        }
    }
}
