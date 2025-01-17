using System;
using myGame.Code.Services.UIManagerController;
using myGame.Code.State.GameResources;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace myGame.Code.Gameplay.UIRoot
{
    public class UIGamePlayRootView : UIView<UIGamePlayRootController>
    {
        public Button AddResSoft;
        public Button SpendResSoft;
        public Transform ResContaner;
        public TextMeshProUGUI ResText;
        public Transform UIScene => _uiScene;
        private Transform UIPupop => _uiPupop;
        
        [SerializeField] private Transform _uiScene;
        [SerializeField] private Transform _uiPupop;

        protected override void OnInitialized()
        {
            AddResSoft.onClick.AddListener(Controller.AddResourceHendler);
            SpendResSoft.onClick.AddListener(() =>Controller.CreatePopupMenu(_uiPupop));
            Controller.ResourceTrackers.ForEach(x => CriateRes(x.ResourceType,ResContaner));
            Controller.SoftCurrent.Subscribe(x => ResText.text = x.ToString());
        }

        public void Update()
        {
            
        }

        private void CriateRes(ResourceType type,Transform parent)
        {
            Controller.CreateVeiwRes(type, parent);
        }
    }
}