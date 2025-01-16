using System;
using R3;

namespace myGame.Code.Gameplay.Services.WorldManagerService
{
    public abstract class WorldRootController : IDisposable
    {
        protected CompositeDisposable _disposables = new CompositeDisposable();
        public abstract void Run();
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}