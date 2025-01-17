using UnityEngine;

namespace myGame.Code.Gameplay.Services.WorldManagerService
{
    public interface IWorldManager
    {
        public TView Criate<TController, TView>(string resourcePath, Transform parent = null)  
            where TController : WorldRootController
            where TView : WorldView<TController>;
    }
}