using UnityEngine;

namespace myGame.Code.Gameplay.Services.WorldManagerService
{
    public interface IWorldManager
    {
        public TView Criate<TController, TView>(GameObject uiPrefab, Transform parent = null)  
            where TController : WorldRootController
            where TView : WorldView<TController>;
    }
}