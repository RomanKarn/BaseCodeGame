using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myGame.Code.Services.CoroutineController
{
    public class CoroutinesService : MonoBehaviour , ICoroutineService
    {
        private GameObject _instens;
        private readonly Dictionary<IEnumerator, Coroutine> _activeCoroutines = new Dictionary<IEnumerator, Coroutine>();
        
        public Coroutine StartTrackedCoroutine(IEnumerator routine)
        {
            Coroutine coroutine = StartCoroutine(TrackCoroutine(routine));
            _activeCoroutines[routine] = coroutine;
            if (coroutine != null)
            {
                _activeCoroutines[routine] = coroutine;
            }
            else
            {
                Debug.LogError("Coroutine could not be started. Check the context or object state.");
            }
            return coroutine;
        }

        public void StopAllTrackedCoroutines()
        {
            foreach (var pair in _activeCoroutines)
            {
                StopCoroutine(pair.Value);
            }
            _activeCoroutines.Clear();
        }

        private IEnumerator TrackCoroutine(IEnumerator routine)
        {
            yield return StartCoroutine(routine); // Ожидаем завершения
            _activeCoroutines.Remove(routine); // Удаляем из списка
        }

    }
}

