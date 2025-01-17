using R3;

namespace myGame.Code.State.GameResources
{
    public class ResourceDataProxy
    {
        public readonly ResourceData Origin;
        public readonly ReactiveProperty<int> Amount;
        public ResourceType ResourceType => Origin.ResourceType;
        public ResourceDataProxy(ResourceData data)
        {
            Origin = data;
            Amount = new ReactiveProperty<int>(data.Amount);
            Amount.Subscribe(newValue => data.Amount = newValue);
        }
    }
}