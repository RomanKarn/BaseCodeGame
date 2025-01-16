using myGame.Code.Gameplay.UIRoot;
using myGame.Code.Services.UIManagerController;
using Zenject;
using R3;

namespace myGame.Code.MainMenu.UIRoot.UIMenu
{
    public class UIMenuController : UIRootController
    {
        private MainMenuEntryPoint _mainMenuEntryPoint;
        
        [Inject]
        public void Constract(MainMenuEntryPoint mainMenuEntryPoint)
        {
               _mainMenuEntryPoint = mainMenuEntryPoint;
        }
        public override void Run()
        {
            
        }
        public void HandlerLoadGamePlay()
        {
            _mainMenuEntryPoint.ExitMainMenu.OnNext(Unit.Default);
        }

        
    }
}