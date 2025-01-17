using System.Threading.Tasks;

namespace myGame.Code.Settings
{
    public interface ISettingsProvider
    {
        GameSettings GameSettings { get; }
        ApplicationSettings ApplicationSettings { get; }
        
        GameSettings LoadGameSettings();
    }
}