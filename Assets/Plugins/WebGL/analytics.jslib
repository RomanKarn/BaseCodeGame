mergeInto(LibraryManager.library, {
    _counterId: null,
     TrackInitializeAnalytics: function(counterIdParam) {
            if (typeof ym === 'undefined') {
                console.error('Yandex.Metrika script not loaded');
                return;
            }
            _counterId = counterIdParam;
            ym(_counterId, "init", {
                                clickmap:true,
                                trackLinks:true,
                                accurateTrackBounce:true
                           });
            console.log("Yandex.Metrika initialized with counter ID:", counterId);
        },
    TrackEvent: function (eventNamePtr) {
        if (typeof ym === 'undefined') {
            console.error('Yandex.Metrika script not loaded');
            return;
        }
        var eventName = UTF8ToString(eventNamePtr);
        ym(_counterId, "reachGoal", eventName);
        console.log("Event tracked:", eventName);
    },
    TrackEventForDate: function(eventName, eventData) {
        const name = UTF8ToString(eventName);
        const data = UTF8ToString(eventData);
        console.log(`Event Sent: ${name}, Data: ${data}`);
        if (typeof ym === 'undefined') {
                    console.error('Yandex.Metrika script not loaded');
                    return;
                }
        if (window.ym) {
            ym(_counterId, 'reachGoal', name, JSON.parse(data));
        }
    }
});