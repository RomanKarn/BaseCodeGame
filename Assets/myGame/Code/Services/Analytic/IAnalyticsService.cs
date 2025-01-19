namespace myGame.Code.Services.Analytic
{
    public interface IAnalyticsService
    {
        void Initialize(int counterId);
        void TrackGoal(string eventName);
        void TrackEventWithData(string eventName, object eventData);
    }
}