using System.Collections.Generic;
using myGame.Code.State.Entities.Plaer;
using myGame.Code.State.GameResources;

namespace myGame.Code.State.Root
{
    public class GameState
    {
        public PlayerEntity Player;
        public List<ResourceData> Resources;
    }
}