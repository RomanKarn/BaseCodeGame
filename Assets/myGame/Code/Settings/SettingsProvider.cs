using System.Threading.Tasks;
using UnityEngine;

namespace myGame.Code.Settings
{
    public class SettingsProvider : ISettingsProvider
    {
        public GameSettings GameSettings => _gameSettings;
        public ApplicationSettings ApplicationSettings { get; }
        private GameSettings _gameSettings;
        
        public SettingsProvider()
        {
            ApplicationSettings = Resources.Load<ApplicationSettings>("Settings/ApplicationSettings");
        }
        
        public GameSettings LoadGameSettings()
        {
            _gameSettings = Resources.Load<GameSettings>("Settings/GameSettings");
            
            return _gameSettings;
        }
    }
}