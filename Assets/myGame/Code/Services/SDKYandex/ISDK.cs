using System.Collections;
namespace myGame.Code.Services.SDKYandex
{
    public interface ISDK
    {
        public void Initialize();
        public IEnumerator InitializeAsync();
        public void GamePlayStartAnaliticSDK();
        public void GamePlayStopAnaliticSDK();
        public void ShowAds();
        public void ShowRevardedAds();
        public void Feedback();
    }
}