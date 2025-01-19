using System.Collections;
using System.Runtime.InteropServices;
using myGame.Code.Services.AudioController;
using myGame.Code.Services.GamePlayAndStop;
using UnityEngine;
using Zenject;

namespace myGame.Code.Services.SDKYandex
{
    public class YandexSdk : MonoBehaviour, ISDK
    {
        private bool _lodingSDKYandex = true;
        private IAudioService _audioService;
        private IGamePlayAndStopSerice _gamePlayAndStopSerice;

        [DllImport("__Internal")]
        private static extern void TrackInitSDK();

        [DllImport("__Internal")]
        private static extern void TrackShowADS();

        [DllImport("__Internal")]
        private static extern void TrackShowRevardedADS();

        [DllImport("__Internal")]
        private static extern void TrackFeedback();

        [DllImport("__Internal")]
        private static extern void TrackGameStartPlay();

        [DllImport("__Internal")]
        private static extern void TrackGameStopPlay();


        [Inject]
        public void Constract(IAudioService audioService, IGamePlayAndStopSerice gamePlayAndStopSerice)
        {
            _audioService = audioService;
            _gamePlayAndStopSerice = gamePlayAndStopSerice;
        }

        public void Initialize()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackInitSDK();
#else
            _lodingSDKYandex = false;
            Debug.Log($"[ADS] Event: ShowADS");
#endif
        }

        public void ShowAds()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackShowADS();
#else
            Debug.Log($"[ADS] Event: ShowADS");
#endif
        }

        public void ShowRevardedAds()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackShowRevardedADS();
#else
            Debug.Log($"[ADS]] Event: ShowRevardedADS");
#endif
        }

        public void Feedback()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackFeedback();
#else
            Debug.Log($"[ADS]] Event: Feedback");
#endif
        }

        public void GamePlayStartAnaliticSDK()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackGameStartPlay();
#else
            Debug.Log($"[ADS]] Event: GameStartPlay");
#endif
        }

        public void GamePlayStopAnaliticSDK()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        TrackGameStopPlay();
#else
            Debug.Log($"[ADS]] Event: GameStopPlay");
#endif
        }

        public IEnumerator InitializeAsync()
        { 
            while (_lodingSDKYandex)
            {
                yield return null;
            }

        }

        public void SDKYndexInitSucses()
        {
            _lodingSDKYandex = false;
        }

        public void GamePlay()
        {
            _gamePlayAndStopSerice.StartAllGame();
            _audioService.EnableOrDisablesoundAndMusic(true);
        }

        public void GameStop()
        {
            _gamePlayAndStopSerice.StopAllGame();
            _audioService.EnableOrDisablesoundAndMusic(false);
        }
    }
}