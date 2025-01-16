using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay.Services.WorldManagerService
{
    public class WorldManager : IWorldManager
    {
        private readonly DiContainer _container;

        [Inject]
        public WorldManager(DiContainer container)
        {
            _container = container;
        }

        public TView Criate<TController, TView>(GameObject uiPrefab, Transform parent = null)  
            where TController : WorldRootController
            where TView : WorldView<TController>
        {
            // Создаём экземпляр World-префаба
            var worldInstance = _container.InstantiatePrefab(uiPrefab, parent);

            // Получаем WorldView
            var worldView = worldInstance.GetComponent<TView>();
            if (worldView == null)
            {
                Debug.LogError($"World Prefab {uiPrefab.name} does not have a component of type {typeof(TView).Name}.");
                return null;
            }

            // Создаём контроллер
            var worldController = _container.Instantiate<TController>();
            worldView.Initialize(worldController);

            Debug.Log($"World {typeof(TView).Name} created with {typeof(TController).Name}.");
            return worldView;
        }
    }
}