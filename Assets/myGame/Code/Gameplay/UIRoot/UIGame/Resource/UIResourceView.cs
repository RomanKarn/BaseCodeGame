using myGame.Code.Services.UIManagerController;
using TMPro;
using R3;

namespace myGame.Code.Gameplay.UIRoot.UIGame.Resource
{
    public class UIResourceView : UIView<UIResourceController>
    {
        public TextMeshProUGUI Value;

        protected override void OnInitialized()
        {
            Controller.Value.Subscribe(x =>
            {
                Value.text = x.ToString();
            } );
        }
    }
}