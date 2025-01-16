using myGame.Code.Gameplay.UIRoot;
using UnityEngine;

namespace myGame.Code.Services.UIManagerController
{
    public interface IUIManager
    {
        public TView ShowUI<TController, TView>(GameObject uiPrefab, Transform parent = null)  
            where TController : UIRootController
            where TView : UIView<TController>;
    }
}
