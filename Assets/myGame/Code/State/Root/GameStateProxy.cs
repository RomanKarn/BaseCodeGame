using System.Linq;
using myGame.Code.State.Entities.Plaer;
using myGame.Code.State.GameResources;
using R3;
using ObservableCollections;

namespace myGame.Code.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        
        public ReactiveProperty<PlayerEntityProxy> Player { get; }
        public ObservableList<ResourceDataProxy> Resources { get; } = new();
        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;
            var player = new PlayerEntityProxy(gameState.Player);
            Player = new ReactiveProperty<PlayerEntityProxy>(player);
            
            Player.Skip(1).Subscribe(value => gameState.Player = value.Origin);

            InitResources(_gameState);
        }
        private void InitResources(GameState gameState)
        {
            gameState.Resources.ForEach(resourceData => Resources.Add(new ResourceDataProxy(resourceData)));
            
            Resources.ObserveAdd().Subscribe(e =>
            {
                var addedResource = e.Value;
                gameState.Resources.Add(addedResource.Origin);
            });
            
            Resources.ObserveRemove().Subscribe(e =>
            {
                var removedResource = e.Value;
                var removedResourceData = gameState.Resources.FirstOrDefault(b => b.ResourceType == removedResource.ResourceType);
                gameState.Resources.Remove(removedResourceData);
            });
        }         
    }
}