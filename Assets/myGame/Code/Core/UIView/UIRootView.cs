using UnityEngine;

namespace myGame.Code.Core.UIView
{
    public class UIRootView : MonoBehaviour
    {
        public Transform SceneContainer => _sceneContainer.transform;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private GameObject _sceneContainer;
        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }
        
    }
}