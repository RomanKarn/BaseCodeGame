using UnityEngine;

namespace myGame.Code.Gameplay.Services.WorldManagerService
{
    public abstract class WorldView<TController> : MonoBehaviour where TController : WorldRootController
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