using UnityEngine;

namespace myGame.Code.Settings.Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerLevelSettings", menuName = "Game Settings/Player/Player Level Settings")]
    public class PlayerLevelSettings : ScriptableObject
    {
        public int Level;
        public int Healse;
    }
}