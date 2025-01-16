using System;
using myGame.Code.Core;
using myGame.Code.MainMenu;
using UnityEngine;

namespace myGame.Code.Gameplay
{
    public class GamePlayEnterParams : SceneEnterParams
    {
        public int Lavel;
        public GamePlayEnterParams(int collButton) : base(Scenes.GAMEPLAY)
        {
            Lavel = collButton;
        }

    }
}