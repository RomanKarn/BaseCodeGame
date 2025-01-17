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

        public TView Criate<TController, TView>(string resourcePath, Transform parent = null)  
            where TController : WorldRootController
            where TView : WorldView<TController>
        {
            var worldPrefab = Resources.Load<GameObject>(resourcePath);
            // Создаём экземпляр World-префаба
            var worldInstance = _container.InstantiatePrefab(worldPrefab, parent);

            // Получаем WorldView
            var worldView = worldInstance.GetComponent<TView>();
            if (worldView == null)
            {
                Debug.LogError($"World Prefab {worldPrefab.name} does not have a component of type {typeof(TView).Name}.");
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