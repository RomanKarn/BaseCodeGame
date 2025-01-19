using Assets.SimpleLocalization.Scripts;
using myGame.Code.State;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace myGame.Code.Services.LanguageLocalaze
{
    public class LanguageLocalazeServices : ILanguageLocalazeServices
    {
        private readonly IGameStateProvider _gameStateProvider;

        [Inject]
        public LanguageLocalazeServices(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
        }

        public void LoadLanguage()
        {
            LocalizationManager.Language = _gameStateProvider.SettingsState.LanguageLocalaze.Value;
        }
        public void LanguageSelected(string language)
        {
            LocalizationManager.Language = language;
            _gameStateProvider.SettingsState.LanguageLocalaze.Value = language;
            _gameStateProvider.SaveSettingsState();
        }
    }
}