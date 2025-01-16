using System.Collections;
using UnityEngine;

namespace myGame.Code.Services.CoroutineController
{
    public interface ICoroutineService
    {
        Coroutine StartTrackedCoroutine(IEnumerator routine);
        void StopAllTrackedCoroutines();
    }
}

