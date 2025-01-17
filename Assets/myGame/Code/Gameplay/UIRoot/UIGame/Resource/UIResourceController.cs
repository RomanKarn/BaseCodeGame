using myGame.Code.Services.UIManagerController;
using R3;

namespace myGame.Code.Gameplay.UIRoot.UIGame.Resource
{
    public class UIResourceController : UIRootController
    {
        public ReactiveProperty<int> Value { get; private set; } = new ReactiveProperty<int>();
        public override void Initialize<T>(T objectToInject)
        {
            if (objectToInject is Observable<int> reactiveObservable)
            {
                reactiveObservable.Subscribe(x => Value.Value = x ).AddTo(_disposables); 
            }
        }

        public override void Run()
        {
          
        }
    }
}