using System;
using myGame.Code.Core;
using UnityEngine;

namespace myGame.Code.MainMenu
{
    public class MainMenuEnterParams : SceneEnterParams
    {
        public int CollButton;
        public MainMenuEnterParams(int collButton) : base(Scenes.MAINMENU)
        {
            CollButton = collButton;
        }
    }
}