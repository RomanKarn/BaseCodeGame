using System.Collections.Generic;
using UnityEngine;

namespace myGame.Code.Services.AudioController
{
    public class FactoryAudio
    {
        private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

        public FactoryAudio()
        {
            //LoadClip(AudioName.POP_UI, "Audio/Sound/pop_ui");
        }
        
        private void LoadClip(string key, string path)
        {
            var request = Resources.Load<AudioClip>(path);
        
            if (request is AudioClip clip)
            {
                _audioClips[key] = clip;
            }
            else
            {
                Debug.LogError($"Failed to load AudioClip at path: {path}");
            }
        }
        public AudioClip ReturnAudioClip(string clipName)
        {
            if (_audioClips.TryGetValue(clipName, out AudioClip clip))
            {
                return clip;
            }

            Debug.LogWarning($"Audio clip with name '{clipName}' not found.");
            return null;
        }
    }
}