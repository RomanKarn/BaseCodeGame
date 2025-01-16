using myGame.Code.MainMenu;

namespace myGame.Code.Gameplay
{
    public class GamePlayExitParams
    {
        public SceneEnterParams TargetSceneEnterParams { get; }

        public GamePlayExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}