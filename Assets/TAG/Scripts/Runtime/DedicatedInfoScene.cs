using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TAG.Runtime
{
    public sealed class DedicatedInfoScene : MonoBehaviour
    {
        [SerializeField] private string title = "HOW TO PLAY";
        [SerializeField, TextArea] private string body = "Run, jump, dash, grapple, survive the tag squad, earn XP, and unlock new maps.";

        private void Start()
        {
            var canvasGo = new GameObject($"{title} Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGo.AddComponent<GraphicRaycaster>();
            CreateText(canvasGo.transform, "Title", new Vector2(0.5f, 1f), new Vector2(0f, -90f), title, 54, TextAnchor.UpperCenter);
            CreateText(canvasGo.transform, "Body", new Vector2(0.5f, 0.5f), Vector2.zero, body, 26, TextAnchor.MiddleCenter);
            AddButton(canvasGo.transform, "BACK", () => SceneManager.LoadScene("MainMenu"));
        }

        private void AddButton(Transform parent, string label, UnityEngine.Events.UnityAction action)
        {
            var go = new GameObject(label);
            go.transform.SetParent(parent, false);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0f);
            rect.anchorMax = new Vector2(0.5f, 0f);
            rect.anchoredPosition = new Vector2(0f, 90f);
            rect.sizeDelta = new Vector2(520f, 86f);
            go.AddComponent<Image>().color = new Color(0.05f, 0.42f, 0.72f, 0.92f);
            var button = go.AddComponent<Button>();
            button.onClick.AddListener(action);
            CreateText(go.transform, "Label", new Vector2(0.5f, 0.5f), Vector2.zero, label, 28, TextAnchor.MiddleCenter);
        }

        private Text CreateText(Transform parent, string name, Vector2 anchor, Vector2 position, string text, int size, TextAnchor alignment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(900f, 520f);
            var label = go.AddComponent<Text>();
            label.text = text;
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = size;
            label.alignment = alignment;
            label.color = Color.white;
            return label;
        }
    }
}
