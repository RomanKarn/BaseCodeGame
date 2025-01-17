using myGame.Code.State.GameResources;
using ObservableCollections;
using R3;

namespace myGame.Code.Gameplay.Services.ResourceService
{
    public interface IResourcesService
    {
        public ObservableList<ResourceTracker> Resources { get; }
        public bool AddResources(ResourceType resourceType, int amount);
        
        public bool TrySpendResources(ResourceType resourceType, int amount);

        public bool IsEnoughResources(ResourceType resourceType, int amount);

        public Observable<int> ObserveResource(ResourceType resourceType);
    }
}