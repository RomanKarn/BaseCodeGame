mergeInto(LibraryManager.library, {
    TrackInitSDK: function() {
           YaGames.init()
               .then((ysdk) => {
                   // Сообщаем платформе, что игра загрузилась и можно начинать играть.
                   ysdk.features.LoadingAPI.ready()
                   ysdk.features.GameplayAPI.stop();
                   window.myGameInstance.SendMessage('YandexSdkYandex', 'SDKYndexInitSucses');
               })
               .catch(console.error);
            },
   TrackShowADS: function() {
          if (ysdk && ysdk.adv) {
                  ysdk.adv.showFullscreenAdv({
                      callbacks: {
                          onOpen: () => {
                            ysdk.features.GameplayAPI.stop();
                            window.myGameInstance.SendMessage('YandexSdkYandex', 'GameStop');
                            console.log("Yandex SDK OPEN TrackShowADS");
                        },
                          onClose: wasShown => {
                            window.myGameInstance.SendMessage('YandexSdkYandex', 'GamePlay');
                            ysdk.features.GameplayAPI.start();
                            console.log("Yandex SDK CLOSE TrackShowADS");
                        },
                            onError: error => console.error("Ad error:", error),
                      }
                  });
              } else {
                  console.error("Yandex SDK not ready for ads");
              }
          },
   TrackShowRevardedADS: function() {
      if (ysdk && ysdk.adv) {
                 ysdk.adv.showRewardedVideo({
                          callbacks: {
                              onOpen: () => {
                              ysdk.features.GameplayAPI.stop();
                              window.myGameInstance.SendMessage('YandexSdkYandex', 'GameStop');
                              console.log("Yandex SDK OPEN TrackShowRevardedADS");
                             },
                              onRewarded: () => console.log("Reward granted"),
                              onClose: () => {
                                 window.myGameInstance.SendMessage('YandexSdkYandex', 'GamePlay');
                                 ysdk.features.GameplayAPI.start();
                                 console.log("Yandex SDK CLOSE TrackShowRevardedADS");
                              },
                              onError: error => console.error("Rewarded Ad error:", error),
                          }
                      });
          } else {
              console.error("Yandex SDK not ready for ads");
          }
   },
   TrackFeedback: function() {
     if (ysdk && ysdk.feedback) {
             ysdk.feedback.canReview().then(result => {
                 if (result.value) {
                     ysdk.feedback.requestReview().then(() => {
                         console.log("Feedback requested");
                     }).catch(error => {
                         console.error("Feedback error:", error);
                     });
                 } else {
                     console.log("Feedback not available");
                 }
             });
         } else {
             console.error("Yandex SDK not ready for rewarded ads");
         }
   },
   TrackGameStartPlay: function() {
     if (ysdk && ysdk.feedback) {
             ysdk.features.GameplayAPI.start();
              console.log("Game Start");
         } else {
             console.error("Yandex SDK not ready for rewarded ads");
         }
   },
   TrackGameStopPlay: function() {
     if (ysdk && ysdk.feedback) {
             ysdk.features.GameplayAPI.stop();
              console.log("Game Stop");
         } else {
             console.error("Yandex SDK not ready for rewarded ads");
         }
   }
});