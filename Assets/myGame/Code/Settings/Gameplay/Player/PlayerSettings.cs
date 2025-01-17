using System.Collections.Generic;
using UnityEngine;

namespace myGame.Code.Settings.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game Settings/Player/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public List<PlayerLevelSettings> PlayerLevel;
    }
}