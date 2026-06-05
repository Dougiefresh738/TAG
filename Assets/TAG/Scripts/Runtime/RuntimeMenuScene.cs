using System.Linq;
using TAG.Core;
using TAG.Progression;
using TAG.Save;
using TAG.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TAG.Runtime
{
    public sealed class RuntimeMenuScene : MonoBehaviour
    {
        [SerializeField] private GameDataCatalog catalog;
        private PlayerSaveData save;
        private readonly UnlockService unlocks = new();
        private Transform content;
        private Text header;

        private void Start()
        {
            catalog = catalog != null ? catalog : GameBootstrap.Instance != null ? GameBootstrap.Instance.Catalog : Resources.Load<GameDataCatalog>("TAG/GameDataCatalog");
            catalog ??= TAG.Content.RuntimeDefaultContent.Catalog();
            save = GameBootstrap.Instance != null ? GameBootstrap.Instance.SaveData : new PlayerSaveData();
            FindObjectOfType<AnalyticsService>()?.Track("main_menu_open");
            BuildMenu();
            ShowHome();
        }

        private void BuildMenu()
        {
            var canvasGo = new GameObject("TAG AAA Menu Canvas");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGo.AddComponent<GraphicRaycaster>();

            header = CreateText(canvasGo.transform, "TAG Header", new Vector2(0.5f, 1f), new Vector2(0f, -70f), "TAG", 76, TextAnchor.UpperCenter);
            content = new GameObject("Menu Content").transform;
            content.SetParent(canvasGo.transform, false);
            var rect = content.gameObject.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(900f, 1250f);
            rect.anchoredPosition = Vector2.zero;
            var layout = content.gameObject.AddComponent<VerticalLayoutGroup>();
            layout.childAlignment = TextAnchor.MiddleCenter;
            layout.spacing = 18f;
            layout.childControlHeight = false;
        }

        private void ShowHome()
        {
            Clear();
            header.text = $"TAG  •  Level {save.accountLevel}";
            AddButton("PLAY", ShowMaps);
            AddButton("CHARACTER COLLECTION", ShowCollection);
            AddButton("BATTLE PASS", ShowBattlePass);
            AddButton("HOW TO PLAY", () => SceneManager.LoadScene("HowToPlay"));
            AddButton("SETTINGS", () => SceneManager.LoadScene("Settings"));
            AddButton("PROFILE", () => SceneManager.LoadScene("Profile"));
        }

        private void ShowMaps()
        {
            Clear();
            header.text = "SELECT MAP";
            foreach (var map in catalog.maps)
            {
                var unlocked = unlocks.IsMapUnlocked(save, map);
                var label = unlocked ? $"▶ {map.displayName}" : $"🔒 {map.displayName} — Level {map.unlockLevel} ({unlocks.MapUnlockProgress(save, map):P0})";
                AddButton(label, unlocked ? () => SceneManager.LoadScene("Game") : null);
            }
            AddButton("BACK", ShowHome);
        }

        private void ShowCollection()
        {
            Clear();
            header.text = "CREATURE COLLECTION";
            foreach (var creature in catalog.creatures)
            {
                var owned = save.unlockedCreatureIds.Contains(creature.creatureId);
                AddButton($"{(owned ? "★" : "☆")} {creature.displayName}  •  {creature.rarity}  •  {creature.personality}", null);
            }
            AddButton("BACK", ShowHome);
        }

        private void ShowBattlePass()
        {
            Clear();
            header.text = "BATTLE PASS";
            FindObjectOfType<AnalyticsService>()?.Track("battle_pass_open");
            FindObjectOfType<EconomyService>()?.TryGrantBattlePassReward(save, "free_launch_badge", "premium_launch_skin");
            AddButton($"Season {save.battlePass.seasonNumber} • Tier {save.battlePass.tier} • XP {save.battlePass.seasonXp}", null);
            AddButton("Free and premium rewards are cosmetics only — never pay-to-win.", null);
            AddButton("BACK", ShowHome);
        }

        private void Clear()
        {
            foreach (Transform child in content) Destroy(child.gameObject);
        }

        private void AddButton(string label, UnityEngine.Events.UnityAction action)
        {
            var go = new GameObject(label);
            go.transform.SetParent(content, false);
            var rect = go.AddComponent<RectTransform>();
            rect.sizeDelta = new Vector2(820f, 86f);
            var image = go.AddComponent<Image>();
            image.color = action == null ? new Color(0.12f, 0.15f, 0.2f, 0.86f) : new Color(0.05f, 0.42f, 0.72f, 0.92f);
            var button = go.AddComponent<Button>();
            button.interactable = action != null;
            if (action != null) button.onClick.AddListener(action);
            CreateText(go.transform, "Label", new Vector2(0.5f, 0.5f), Vector2.zero, label, 24, TextAnchor.MiddleCenter);
        }

        private Text CreateText(Transform parent, string name, Vector2 anchor, Vector2 position, string text, int size, TextAnchor alignment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(880f, 96f);
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
