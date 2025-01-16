using System;
using R3;
using UnityEngine;

namespace myGame.Code.Services.UIManagerController
{
    public abstract class UIRootController : IDisposable
    {
        protected CompositeDisposable _disposables = new CompositeDisposable();
        public abstract void Run();
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}