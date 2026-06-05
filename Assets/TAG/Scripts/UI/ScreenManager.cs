using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAG.UI
{
    public sealed class ScreenManager : MonoBehaviour
    {
        [SerializeField] private Canvas rootCanvas;
        [SerializeField] private float transitionSeconds = 0.22f;
        private readonly Stack<string> navigationStack = new();
        private readonly Dictionary<string, CanvasGroup> screens = new();

        private void Awake()
        {
            if (rootCanvas == null) rootCanvas = GetComponentInChildren<Canvas>();
            foreach (var group in GetComponentsInChildren<CanvasGroup>(true))
            {
                screens[group.gameObject.name] = group;
                group.gameObject.SetActive(false);
            }
        }

        public void OpenScreen(string screenName)
        {
            if (screens.ContainsKey(screenName))
            {
                if (navigationStack.Count > 0) Hide(navigationStack.Peek());
                navigationStack.Push(screenName);
                Show(screenName);
            }
        }

        public void Back()
        {
            if (navigationStack.Count <= 1) return;
            Hide(navigationStack.Pop());
            Show(navigationStack.Peek());
        }

        public void LoadDedicatedScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        private void Show(string screenName)
        {
            var group = screens[screenName];
            group.gameObject.SetActive(true);
            group.alpha = 1f;
            group.interactable = true;
            group.blocksRaycasts = true;
            group.transform.SetAsLastSibling();
        }

        private void Hide(string screenName)
        {
            var group = screens[screenName];
            group.alpha = 0f;
            group.interactable = false;
            group.blocksRaycasts = false;
            group.gameObject.SetActive(false);
        }
    }
}
