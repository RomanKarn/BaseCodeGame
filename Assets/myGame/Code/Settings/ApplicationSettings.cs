﻿using UnityEngine;

namespace myGame.Code.Settings
{
    [CreateAssetMenu(fileName = "ApplicationSettings", menuName = "Game Settings/New Application Settings")]
    public class ApplicationSettings : ScriptableObject
    {
          public int MusicVolume;
          public int SFXVolume;
          public string LanguageLocalaze;
    }
}