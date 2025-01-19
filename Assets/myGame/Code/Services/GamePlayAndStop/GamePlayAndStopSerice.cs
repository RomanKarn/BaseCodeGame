using myGame.Code.Services.CoroutineController;
using UnityEngine;
using Zenject;

namespace myGame.Code.Services.GamePlayAndStop
{
    public class GamePlayAndStopSerice : IGamePlayAndStopSerice
    {
        private ICoroutineService _coroutines;

        [Inject]
        public void Constract(ICoroutineService coroutines)
        {
           _coroutines = coroutines;
        }
        
        public void StartAllGame()
        {
            Time.timeScale = 1f;
            _coroutines.ResumeAllCoroutines();
        }

        public void StopAllGame()
        {
            Time.timeScale = 0f;
            _coroutines.PauseAllCoroutines();
        }
    }
}