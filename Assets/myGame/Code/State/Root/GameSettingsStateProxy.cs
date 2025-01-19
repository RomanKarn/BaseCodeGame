using R3;

namespace myGame.Code.State.Root
{
    public class GameSettingsStateProxy
    {
        public ReactiveProperty<float> MusicVolume { get; }
        public ReactiveProperty<float> SFXVolume { get; }
        
        public ReactiveProperty<string> LanguageLocalaze { get; }

        public GameSettingsStateProxy(GameSettingsState gameSettingsState)
        {
            MusicVolume = new ReactiveProperty<float>(gameSettingsState.MusicVolume);
            SFXVolume = new ReactiveProperty<float>(gameSettingsState.SFXVolume);
            LanguageLocalaze = new ReactiveProperty<string>(gameSettingsState.LanguageLocalaze);

            MusicVolume.Skip(1).Subscribe(value => gameSettingsState.MusicVolume = value);
            SFXVolume.Skip(1).Subscribe(value => gameSettingsState.SFXVolume = value);
            LanguageLocalaze.Skip(1).Subscribe(value => gameSettingsState.LanguageLocalaze = value);
        }
    }
}