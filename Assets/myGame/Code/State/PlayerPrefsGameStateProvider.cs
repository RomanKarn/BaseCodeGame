﻿using System;
using System.Collections.Generic;
using myGame.Code.Settings;
using myGame.Code.State.Entities.Plaer;
using myGame.Code.State.GameResources;
using myGame.Code.State.Root;
using UnityEngine;
using R3;

namespace myGame.Code.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

        public GameStateProxy GameState { get; private set; }
        public GameSettingsStateProxy SettingsState { get; private set; }

        private GameState _gameStateOrigin;
        private GameSettingsState _gameSettingsStateOrigin;

        public Observable<GameStateProxy> LoadGameState()
        {
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreateGameStateFromSettings();
                Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));

                SaveGameState();    // Сохраним дефолтное состояние
            }
            else
            {
                // Загружаем
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);

                Debug.Log("Game State loaded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<GameSettingsStateProxy> LoadSettingsState(ApplicationSettings appSettings)
        {
            if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY))
            {
                SettingsState = CreateGameSettingsStateFromSettings(appSettings);

                SaveSettingsState();    // Сохраним дефолтное состояние
            }
            else
            {
                // Загружаем
                var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
                _gameSettingsStateOrigin = JsonUtility.FromJson<GameSettingsState>(json);
                SettingsState = new GameSettingsStateProxy(_gameSettingsStateOrigin);
                Debug.Log("Settings State loaded: " + json);
            }

            return Observable.Return(SettingsState);
        }

        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> SaveSettingsState()
        {
            var json = JsonUtility.ToJson(_gameSettingsStateOrigin, true);
            PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);

            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();

            return Observable.Return(true);
        }

        public Observable<GameSettingsStateProxy> ResetSettingsState(ApplicationSettings appSettings)
        {
            SettingsState = CreateGameSettingsStateFromSettings(appSettings);
            SaveSettingsState();

            return Observable.Return(SettingsState);
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            // Состояние по умолчанию из настроек, мы делаем фейк
            _gameStateOrigin = new GameState
            {
                Player = new PlayerEntity()
                {
                    Level = 0
                }, 
                Resources = new List<ResourceData>()
                {
                    new() { Amount = 0, ResourceType = ResourceType.SoftCurrency },
                    new() { Amount = 0, ResourceType = ResourceType.HardCurrency }
                }
                
            };

            return new GameStateProxy(_gameStateOrigin);
        }

        private GameSettingsStateProxy CreateGameSettingsStateFromSettings(ApplicationSettings appSettings)
        {
            // Состояние по умолчанию из настроек, мы делаем фейк
            _gameSettingsStateOrigin = new GameSettingsState()
            {
                MusicVolume = appSettings.MusicVolume,
                SFXVolume = appSettings.SFXVolume,
                LanguageLocalaze = appSettings.LanguageLocalaze
            };

            return new GameSettingsStateProxy(_gameSettingsStateOrigin);
        }

    }
}