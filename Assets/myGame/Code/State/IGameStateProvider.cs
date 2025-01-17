using System.Collections.Generic;
using myGame.Code.Settings;
using myGame.Code.State.Root;
using R3;

namespace myGame.Code.State
{
    public interface IGameStateProvider
    {
        public GameStateProxy GameState { get; }
        public GameSettingsStateProxy SettingsState { get; }
        
        public Observable<GameStateProxy> LoadGameState();
        public Observable<GameSettingsStateProxy> LoadSettingsState(ApplicationSettings appSettings);
        
        public Observable<bool> SaveGameState();
        public Observable<bool> SaveSettingsState();
        
        public Observable<bool> ResetGameState();
        public Observable<GameSettingsStateProxy> ResetSettingsState(ApplicationSettings appSettings);
    }
}