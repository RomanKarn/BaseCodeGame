using System.Collections.Generic;
using myGame.Code.Gameplay.Services.ResourceService;
using myGame.Code.Gameplay.UIRoot.UIGame.Resource;
using myGame.Code.Gameplay.UIRoot.UIGame.UIPopupMenu;
using myGame.Code.Services.UIManagerController;
using myGame.Code.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay.UIRoot
{
    public class UIGamePlayRootController : UIRootController
    {
        private IResourcesService _resourcesService;
        private IUIManager _uiManager;
        public ObservableList<ResourceTracker> ResourceTrackers { get; private set; } = new ObservableList<ResourceTracker>();
        public ReactiveProperty<int> SoftCurrent { get; private set; } = new ReactiveProperty<int>();
        
        [Inject]
        private void Constract(
            IResourcesService resourcesService,
            IUIManager uiManager)
        {
            _resourcesService = resourcesService;
            _uiManager = uiManager;
        }

        public override void Run()
        {
            ResourceTrackers = _resourcesService.Resources;
            _resourcesService.ObserveResource(ResourceType.SoftCurrency).Subscribe(x => SoftCurrent.Value = x).AddTo(_disposables);
        }

        public void AddResourceHendler()
        {
            var random = Random.Range(0, 330);
            _resourcesService.AddResources(ResourceType.SoftCurrency, random);
        }
        public void SpendResourceHendler()
        {
            var random = Random.Range(0, 330);
            _resourcesService.TrySpendResources(ResourceType.SoftCurrency, random);
        }

        public void CreatePopupMenu(Transform parent)
        {
            _uiManager.ShowUI<UIMenuPopupController, UIMenuPopupVeiw, ObservableList<ResourceTracker>>(
                UIPathPrefabs.UIMenuPopup, 
                parent,
                true,
                ResourceTrackers);
        }
        
        public void CreateVeiwRes(ResourceType type, Transform parent)
        {
           _uiManager.ShowUI<UIResourceController, UIResourceView, Observable<int>>(
               UIPathPrefabs.UIResource, 
               parent, 
               false, 
               _resourcesService.ObserveResource(type));
        }
    }
}