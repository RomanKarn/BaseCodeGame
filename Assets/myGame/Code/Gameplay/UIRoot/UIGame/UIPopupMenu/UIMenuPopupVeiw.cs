using myGame.Code.Services.UIManagerController;
using UnityEngine;
using UnityEngine.UI;

namespace myGame.Code.Gameplay.UIRoot.UIGame.UIPopupMenu
{
    public class UIMenuPopupVeiw : UIView<UIMenuPopupController>
    {
        public Button ShowRes;
        public Button CloseButton;
        public Transform ResParent;
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ShowRes.onClick.AddListener(() => Controller.ShowResursHendler(ResParent));
            CloseButton.onClick.AddListener(() =>Destroy(gameObject));
        }
        
        
    }
}