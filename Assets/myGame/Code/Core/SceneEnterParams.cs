namespace myGame.Code.MainMenu
{
    public class SceneEnterParams
    {
        public string SceneName;

        public SceneEnterParams(string sceneName)
        {
            SceneName = sceneName;
        }

        public T As<T>() where T : SceneEnterParams
        {
            return (T)this;
        }
    }
}