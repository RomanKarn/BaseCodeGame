using myGame.Code.Gameplay.UIRoot;
using UnityEngine;

namespace myGame.Code.Services.UIManagerController
{
    public interface IUIManager
    {
        public TView ShowUI<TController, TView, TObjectInject>(
            string resourcePath, 
            Transform parent = null, 
            bool clearParent = false,
            TObjectInject injector = null)  
            where TController : UIRootController
            where TView : UIView<TController>
            where TObjectInject : class;

        public TView ShowUI<TController, TView>(
            string resourcePath,
            Transform parent = null,
            bool clearParent = false)
            where TController : UIRootController
            where TView : UIView<TController>;
    }
}
