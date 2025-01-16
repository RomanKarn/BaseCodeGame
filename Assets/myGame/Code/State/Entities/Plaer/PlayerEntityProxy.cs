using System;
using R3;

namespace myGame.Code.State.Entities.Plaer
{
    public class PlayerEntityProxy
    {
        public int Id;
        public PlayerEntity Origin;
        
        public ReactiveProperty<int> Level { get; }

        public PlayerEntityProxy(PlayerEntity playerEntity)
        {
            Origin = playerEntity;
            Id = playerEntity.Id;
            
            Level = new ReactiveProperty<int>(playerEntity.Level);
            
            Level.Skip(1).Subscribe(value => playerEntity.Level = value);
        }
    }
}