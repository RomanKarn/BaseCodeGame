using myGame.Code.Settings;
using myGame.Code.State;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.Services.AudioController
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        public ReactiveProperty<float> MusicVolume { get; private set; }
        public ReactiveProperty<float> SFXVolume { get; private set; }
        
        private IGameStateProvider _gameStateProvider;
        private ISettingsProvider _settingsProvider;
        
        private AudioSource _musicSource;
        private AudioSource _sfxSource;
        
        private FactoryAudio _factoryAudio;
        
        private float _musicVolumeBase;
        private float _sfxVolumeBase;
    
        private float _musicVolume;
        private float _sfxVolume;

        [Inject]
        public void Constract(IGameStateProvider gameStateProvider, ISettingsProvider settingsProvider)
        {
            _gameStateProvider = gameStateProvider;
            _settingsProvider = settingsProvider;
            _factoryAudio = new FactoryAudio();
            _musicSource = gameObject.AddComponent<AudioSource>();
            _sfxSource = gameObject.AddComponent<AudioSource>();

        }

        public void Initialize()
        {
            _musicVolumeBase = _settingsProvider.ApplicationSettings.MusicVolume;
            _sfxVolumeBase = _settingsProvider.ApplicationSettings.SFXVolume;
            MusicVolume = _gameStateProvider.SettingsState.MusicVolume;
            SFXVolume = _gameStateProvider.SettingsState.SFXVolume;
            _musicSource.volume = _musicVolume = MusicVolume.Value;
            _sfxSource.volume  = _sfxVolume = SFXVolume.Value;
        }
        
        public void PlayMusic(string clipName, bool loop = true)
        {
            var clip = _factoryAudio.ReturnAudioClip(clipName);
            if (_musicSource.clip == clip) return;

            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void PlaySFX(string clipName)
        {
            var clip = _factoryAudio.ReturnAudioClip(clipName);
            
            _sfxSource.PlayOneShot(clip);
        }

        public void SetMusicVolume(float volume)
        {
            if (volume > 0)
            {
                _musicVolume = _musicVolumeBase;
            }
            else
            {
                _musicVolume = 0;
            }
            MusicVolume.Value = _musicVolume;
            _gameStateProvider.SaveSettingsState();
            UpdateVolumes();
        }

        public void SetSFXVolume(float volume)
        {
            if (volume > 0)
            {
                _sfxVolume = _sfxVolumeBase;
            }
            else
            {
                _sfxVolume = 0;
            }
            SFXVolume.Value = _sfxVolume;
            _gameStateProvider.SaveSettingsState();
            UpdateVolumes();
        }
        
        public void EnableOrDisablesoundAndMusic(bool enable)
        {
            _musicSource.volume = enable ? _musicVolume : 0;
            _sfxSource.volume = enable ? _sfxVolume : 0;
        }
        private void UpdateVolumes()
        {
            _musicSource.volume = _musicVolume;
            _sfxSource.volume = _sfxVolume;
        }
    }
}