using myGame.Code.State.GameResources;
using R3;

namespace myGame.Code.Gameplay.Services.ResourceService
{
    public class ResourceTracker
    {
        public readonly ResourceType ResourceType;
        public readonly ReadOnlyReactiveProperty<int> Amount;
        public ResourceTracker(ResourceDataProxy resource)
        {
            ResourceType = resource.ResourceType;
            Amount = resource.Amount;
        }
    }
}