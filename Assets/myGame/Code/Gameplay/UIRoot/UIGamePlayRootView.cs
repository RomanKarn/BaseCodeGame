using System;
using myGame.Code.Services.UIManagerController;
using R3;
using TMPro;
using UnityEngine;

namespace myGame.Code.Gameplay.UIRoot
{
    public class UIGamePlayRootView : UIView<UIGamePlayRootController>
    {
        public Transform UIScene => _uiScene;
        private Transform UIPupop => _uiPupop;
        
        [SerializeField] private Transform _uiScene;
        [SerializeField] private Transform _uiPupop;

        protected override void OnInitialized()
        {
            
        }

        public void Update()
        {
        }
    }
}