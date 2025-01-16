using myGame.Code.Services.UIManagerController;
using UnityEngine.UI;

namespace myGame.Code.MainMenu.UIRoot.UIMenu
{
    public class UIMenuView  : UIView<UIMenuController>
    {
        public Button StartGameButton;
        protected override void OnInitialized()
        {
            StartGameButton.onClick.AddListener(Controller.HandlerLoadGamePlay);
        }
    }
}
