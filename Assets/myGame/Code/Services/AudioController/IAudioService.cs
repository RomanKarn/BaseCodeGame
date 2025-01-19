using myGame.Code.Services.CoroutineController;

namespace myGame.Code.Services.AudioController
{
    public interface IAudioService
    {
        public void Initialize();
        public void PlayMusic(string clipName, bool loop = true);
        public void PlaySFX(string clipName);
        public void SetMusicVolume(float volume);
        public void SetSFXVolume(float volume);
        public void EnableOrDisablesoundAndMusic(bool enable);
    }
}