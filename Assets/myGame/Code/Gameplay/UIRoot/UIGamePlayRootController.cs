using System.Collections;
using System.Collections.Generic;
using myGame.Code.Gameplay.Services.ResourceService;
using myGame.Code.Gameplay.UIRoot.UIGame.Resource;
using myGame.Code.Gameplay.UIRoot.UIGame.UIPopupMenu;
using myGame.Code.Services.CoroutineController;
using myGame.Code.Services.GamePlayAndStop;
using myGame.Code.Services.LanguageLocalaze;
using myGame.Code.Services.UIManagerController;
using myGame.Code.State;
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
        private ICoroutineService _coroutineService;
        private IGamePlayAndStopSerice _gamePlayAndStopService;
        private ILanguageLocalazeServices _languageLocalazeServices;
        private IGameStateProvider _gameStateProvider;
        public ObservableList<ResourceTracker> ResourceTrackers { get; private set; } = new ObservableList<ResourceTracker>();
        public ReactiveProperty<int> SoftCurrent { get; private set; } = new ReactiveProperty<int>();
        
        [Inject]
        private void Constract(
            ICoroutineService coroutineService,
            IGamePlayAndStopSerice gamePlayAndStopService,
            IResourcesService resourcesService,
            IUIManager uiManager,
            ILanguageLocalazeServices languageLocalazeServices)
        {
            _resourcesService = resourcesService;
            _uiManager = uiManager;
            _coroutineService = coroutineService;
            _gamePlayAndStopService = gamePlayAndStopService;
            _languageLocalazeServices = languageLocalazeServices;
        }

        public override void Run()
        {
            ResourceTrackers = _resourcesService.Resources;
            _resourcesService.ObserveResource(ResourceType.SoftCurrency).Subscribe(x => SoftCurrent.Value = x).AddTo(_disposables);
        }
        public void GamePlay()
        {
            _gamePlayAndStopService.StartAllGame();
        }
        
        public void GameStop()
        {
            _gamePlayAndStopService.StopAllGame();
        }

        public void Selectlang(string lang)
        {
            _languageLocalazeServices.LanguageSelected(lang);
        }
        public void MoveSicale(Transform transform)
        {
            _coroutineService.StartTrackedCoroutine(MoveCoroutine(transform));
        }

        private IEnumerator MoveCoroutine(Transform transform)
        {
            var distans = 1800;
            var stape = 0.001f;
            var time = 0f;
            while (time < 1f)
            {
                transform.position = Vector3.Lerp(transform.position, transform.TransformPoint(Vector3.down * distans), stape);
                time += stape;
                yield return new WaitForFixedUpdate();
            }
            yield return null;
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