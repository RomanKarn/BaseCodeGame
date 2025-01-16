using myGame.Code.State.Entities.Plaer;
using R3;

namespace myGame.Code.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        
        public ReactiveProperty<PlayerEntityProxy> Player { get; }

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;
            var player = new PlayerEntityProxy(gameState.Player);
            Player = new ReactiveProperty<PlayerEntityProxy>(player);
            
            Player.Skip(1).Subscribe(value => gameState.Player = value.Origin);
        }
              
    }
}