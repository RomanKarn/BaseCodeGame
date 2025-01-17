using myGame.Code.Settings.Gameplay.Player;
using UnityEngine;

namespace myGame.Code.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/New Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public PlayerSettings Player;
    }
}