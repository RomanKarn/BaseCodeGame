using myGame.Code.Gameplay.UIRoot;
using UnityEngine;

namespace myGame.Code.Services.UIManagerController
{
    public abstract class UIView<TController> : MonoBehaviour where TController : UIRootController
    {
        protected TController Controller { get; private set; }

        public void Initialize(TController controller)
        {
            Controller = controller;
            Controller.Run();
            OnInitialized();
        }

        protected virtual void OnInitialized()
        {
            // Логика инициализации конкретного представления
        }
        
        private void OnDestroy()
        {
            Controller.Dispose();
        }
    }
}