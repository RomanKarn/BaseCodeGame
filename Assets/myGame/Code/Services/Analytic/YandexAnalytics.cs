using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;

namespace myGame.Code.Services.Analytic
{
    public class YandexAnalytics : IAnalyticsService
    {
        [DllImport("__Internal")]
        private static extern void TrackInitializeAnalytics(int counterId);
        
        [DllImport("__Internal")]
        private static extern void TrackEvent(string eventName);

        [DllImport("__Internal")]
        private static extern void TrackEventForDate(string eventName, string eventData);
        
        public void Initialize(int counterId)
        {
#if !UNITY_EDITOR
        TrackInitializeAnalytics(counterId);
#else
            Debug.Log($"[Analytics] Initialized with Counter ID: {counterId}");
#endif
        }

        public void TrackGoal(string eventName)
        {
#if !UNITY_EDITOR
        TrackEvent(eventName);
#else
            Debug.Log($"[Analytics] Event Tracked: {eventName}");
#endif
        }

        public void TrackEventWithData(string eventName, object eventData)
        {
#if !UNITY_EDITOR
        string eventDataJson = SerializeEventData(eventData);
        TrackEventForDate(eventName,eventDataJson);
#else
            Debug.Log($"[Analytics] Event: {eventName}, Data: {eventData}");
#endif
        }
        
        private string SerializeEventData(object eventData)
        {
            if (eventData == null)
            {
                return "{}";
            }

            return JsonConvert.SerializeObject(eventData);
        }
    }
}