using System;
using System.Collections.Generic;
using System.Linq;
using myGame.Code.State;
using myGame.Code.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;
using Zenject;

namespace myGame.Code.Gameplay.Services.ResourceService 
{
    public class ResourcesService : IResourcesService
    {
        public ObservableList<ResourceTracker> Resources => _resources;

        private readonly ObservableList<ResourceTracker> _resources = new();
        private readonly Dictionary<ResourceType, ResourceTracker> _resourcesMap = new();
        private readonly IGameStateProvider _gameStateProvider;

        [Inject]
        public ResourcesService(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
            
            var resources = _gameStateProvider.GameState.Resources;
            resources.ForEach(CreateResourceViewModel);
            resources.ObserveAdd().Subscribe(e => CreateResourceViewModel(e.Value));
            resources.ObserveRemove().Subscribe(e => RemoveResourceViewModel(e.Value));
        }

        public bool AddResources(ResourceType resourceType, int amount)
        {
            var requiredResource = _gameStateProvider.GameState.Resources.FirstOrDefault(r => r.ResourceType == resourceType);
            if (requiredResource == null)
            {
                requiredResource = CreateNewResource(resourceType);
            }
            requiredResource.Amount.Value += amount;
            _gameStateProvider.SaveGameState();
            return true;
        }
        
        
        public bool TrySpendResources(ResourceType resourceType, int amount)
        {
            var requiredResource = _gameStateProvider.GameState.Resources.FirstOrDefault(r => r.ResourceType == resourceType);
            if (requiredResource == null)
            {
                Debug.LogError("Trying to spend not existed resource");
                return false;
            }
            if (requiredResource.Amount.Value < amount)
            {
                Debug.LogError(
                    $"Trying to spend more resources than existed ({resourceType}). Exists: {requiredResource.Amount.Value}, trying to spend: {amount}");
                return false;
            }
            requiredResource.Amount.Value -= amount;
            _gameStateProvider.SaveGameState();
            
            return true;
        }
        
        public bool IsEnoughResources(ResourceType resourceType, int amount)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount.CurrentValue >= amount;
            }
            return false;
        }
        
        public Observable<int> ObserveResource(ResourceType resourceType)
        {
            if (_resourcesMap.TryGetValue(resourceType, out var resourceViewModel))
            {
                return resourceViewModel.Amount;
            }
            throw new Exception($"Resource of type {resourceType} doesn't exist");
        }
        
        private ResourceDataProxy CreateNewResource(ResourceType resourceType)
        {
            var newResourceData = new ResourceData
            {
                ResourceType = resourceType,
                Amount = 0
            };
            var newResource = new ResourceDataProxy(newResourceData);
            _gameStateProvider.GameState.Resources.Add(newResource);
            
            return newResource;
        }
        
        private void CreateResourceViewModel(ResourceDataProxy resource)
        {
            var resourceViewModel = new ResourceTracker(resource);
            _resourcesMap[resource.ResourceType] = resourceViewModel;
            
            Resources.Add(resourceViewModel);
        }
        
        private void RemoveResourceViewModel(ResourceDataProxy resource)
        {
            if (_resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel))
            {
                Resources.Remove(resourceViewModel);
                _resourcesMap.Remove(resource.ResourceType);
            }
        }
    }
}