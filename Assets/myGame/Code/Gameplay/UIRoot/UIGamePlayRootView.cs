using System;
using myGame.Code.Services.LanguageLocalaze;
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
        public Button StartGme;
        public Button GameStop;
        
        public Button LangSelectRu;
        public Button LangSelectEn;
        public Button LangSelectTu;
        
        public Transform ResContaner;
        public TextMeshProUGUI ResText;
        public Transform UIScene => _uiScene;
        private Transform UIPupop => _uiPupop;
        
        [SerializeField] private Transform sicale;
        [SerializeField] private Transform _uiScene;
        [SerializeField] private Transform _uiPupop;
        private float _time;

        protected override void OnInitialized()
        {
            AddResSoft.onClick.AddListener(Controller.AddResourceHendler);
            SpendResSoft.onClick.AddListener(() =>Controller.CreatePopupMenu(_uiPupop));
            GameStop.onClick.AddListener(Controller.GameStop);
            StartGme.onClick.AddListener(Controller.GamePlay);
            LangSelectRu.onClick.AddListener(() => Controller.Selectlang(LanguagesLocalazed.RU));
            LangSelectEn.onClick.AddListener(() => Controller.Selectlang(LanguagesLocalazed.EN));
            LangSelectTu.onClick.AddListener(() => Controller.Selectlang(LanguagesLocalazed.TU));
            Controller.ResourceTrackers.ForEach(x => CriateRes(x.ResourceType,ResContaner));
            Controller.MoveSicale(sicale);
        }

        public void Update()
        {
            _time += Time.deltaTime;
            ResText.text = _time.ToString("0.00");
        }

        private void CriateRes(ResourceType type,Transform parent)
        {
            Controller.CreateVeiwRes(type, parent);
        }
    }
}