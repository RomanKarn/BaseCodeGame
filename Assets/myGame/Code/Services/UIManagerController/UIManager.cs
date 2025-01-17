using System;
using myGame.Code.Gameplay.UIRoot;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace myGame.Code.Services.UIManagerController
{
    public class UIManager : IUIManager
    {
        private readonly DiContainer _container;

        [Inject]
        public UIManager(DiContainer container)
        {
            _container = container;
        }

        public TView ShowUI<TController, TView, TObjectInject>(
            string resourcePath, 
            Transform parent = null, 
            bool clearParent = false,
            TObjectInject injector = null) 
            where TController : UIRootController 
            where TView : UIView<TController>
            where TObjectInject : class
        {
            if(parent != null && clearParent)
                ClearParent(parent);
            // Создаём экземпляр UI-префаба
            var uiPrefab = Resources.Load<GameObject>(resourcePath);
            
            var uiInstance = _container.InstantiatePrefab(uiPrefab, parent);

            // Получаем UIView
            var uiView = uiInstance.GetComponent<TView>();
            if (uiView == null)
            {
                Debug.LogError($"UI Prefab {uiPrefab.name} does not have a component of type {typeof(TView).Name}.");
                return null;
            }

            // Создаём контроллер
            var uiController = _container.Instantiate<TController>();
            uiController.Initialize(injector);
            uiView.Initialize(uiController);

            Debug.Log($"UI {typeof(TView).Name} created with {typeof(TController).Name}.");
            return uiView;
        }
        public TView ShowUI<TController, TView>(
            string resourcePath, 
            Transform parent = null, 
            bool clearParent = false) 
            where TController : UIRootController 
            where TView : UIView<TController>
        {
            if(parent != null && clearParent)
                ClearParent(parent);
            // Создаём экземпляр UI-префаба
            var uiPrefab = Resources.Load<GameObject>(resourcePath);
            
            var uiInstance = _container.InstantiatePrefab(uiPrefab, parent);

            // Получаем UIView
            var uiView = uiInstance.GetComponent<TView>();
            if (uiView == null)
            {
                Debug.LogError($"UI Prefab {uiPrefab.name} does not have a component of type {typeof(TView).Name}.");
                return null;
            }

            // Создаём контроллер
            var uiController = _container.Instantiate<TController>();
            uiView.Initialize(uiController);

            Debug.Log($"UI {typeof(TView).Name} created with {typeof(TController).Name}.");
            return uiView;
        }
        private void ClearParent(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Object.Destroy(child.gameObject);
            }
        }

    }
}
