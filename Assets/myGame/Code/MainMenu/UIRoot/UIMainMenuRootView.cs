using myGame.Code.Services.UIManagerController;
using UnityEngine;

namespace myGame.Code.MainMenu.UIRoot
{
    public class UIMainMenuRootView : UIView<UIMainMenuRootController>
    {
        public Transform UIScene => _uiScene;
        private Transform UIPupop => _uiPupop;
        
        [SerializeField] private Transform _uiScene;
        [SerializeField] private Transform _uiPupop;
    }
}