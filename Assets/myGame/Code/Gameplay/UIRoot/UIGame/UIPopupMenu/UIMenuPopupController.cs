using System.Linq;
using myGame.Code.Gameplay.Services.ResourceService;
using myGame.Code.Gameplay.UIRoot.UIGame.Resource;
using myGame.Code.Services.UIManagerController;
using myGame.Code.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay.UIRoot.UIGame.UIPopupMenu
{
    public class UIMenuPopupController : UIRootController
    {
        private IUIManager _uiManager;
        private GamePlayEntryPoint _gamePlayExitPoint;
        public ObservableList<ResourceTracker>  ResourceTrackers { get; private set; } = new ObservableList<ResourceTracker>();
        
        [Inject]
        private void Constract(
            GamePlayEntryPoint gamePlayExitPoint,
            IUIManager uiManager)
        {
            _gamePlayExitPoint = gamePlayExitPoint;
            _uiManager = uiManager;
        }
        public override void Initialize<T>(T objectToInject)
        {
            if (objectToInject is ObservableList<ResourceTracker>  reactiveObservable)
            {
                ResourceTrackers = reactiveObservable;
            }
        }
        public override void Run()
        {
            
        }

        public void ShowResursHendler(Transform parent)
        {
            ResourceTrackers.ForEach(x=>
            {
                CreateVeiwRes(x.ResourceType, parent);
            });
        }

        public void ExitGamePlay()
        {
            _gamePlayExitPoint.ExitGamePlay.OnNext(Unit.Default);
        }
        private void CreateVeiwRes(ResourceType type, Transform parent)
        {
            _uiManager.ShowUI<UIResourceController, UIResourceView, Observable<int>>(
                UIPathPrefabs.UIResource, 
                parent, 
                false, 
                ResourceTrackers.FirstOrDefault(x => x.ResourceType == type)?.Amount);
        }
    }
}