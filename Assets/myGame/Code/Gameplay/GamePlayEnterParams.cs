using System;
using myGame.Code.Core;
using myGame.Code.MainMenu;
using UnityEngine;

namespace myGame.Code.Gameplay
{
    public class GamePlayEnterParams : SceneEnterParams
    {
        
        public GamePlayEnterParams() : base(Scenes.GAMEPLAY)
        {
            
        }

    }
}