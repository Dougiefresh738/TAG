using System.Linq;
using TAG.AI;
using TAG.Characters;
using TAG.Content;
using TAG.Core;
using TAG.Maps;
using UnityEngine;
using UnityEngine.UI;

namespace TAG.Runtime
{
    public sealed class RuntimeGameScene : MonoBehaviour
    {
        [SerializeField] private GameDataCatalog catalog;
        [SerializeField] private ProceduralMaterialPalette palette;
        [SerializeField] private string forcedMapId = "rooftop";
        private CreatureController player;
        private Vector2 move;
        private bool sprint;
        private bool jumpQueued;
        private bool dashQueued;
        private float timer;
        private Text timerText;
        private Text objectiveText;

        private void Start()
        {
            catalog = catalog != null ? catalog : GameBootstrap.Instance != null ? GameBootstrap.Instance.Catalog : Resources.Load<GameDataCatalog>("TAG/GameDataCatalog");
            catalog ??= RuntimeDefaultContent.Catalog();
            palette = palette != null ? palette : Resources.Load<ProceduralMaterialPalette>("TAG/ProceduralPalette");
            palette ??= RuntimeDefaultContent.Palette();
            BuildLevel();
            BuildHud();
        }

        private void Update()
        {
            ReadInput();
            player?.Move(move, sprint, jumpQueued, dashQueued);
            jumpQueued = false;
            dashQueued = false;
            timer += Time.deltaTime;
            if (timerText != null) timerText.text = $"SURVIVE  {timer:0.0}s";
        }

        private void BuildLevel()
        {
            var map = catalog != null ? catalog.maps.FirstOrDefault(m => m.mapId == forcedMapId) ?? catalog.maps.FirstOrDefault() : null;
            ProceduralMapBuilder.Build(map, palette);

            var creature = catalog != null ? catalog.creatures.FirstOrDefault() : null;
            var playerGo = ProceduralCreatureFactory.CreateCreature(creature, palette, true);
            playerGo.transform.position = new Vector3(0f, 1f, -12f);
            player = playerGo.GetComponent<CreatureController>();

            var director = new GameObject("AI Director").AddComponent<AIDirector>();
            SetPrivate(director, "player", playerGo.transform);
            var enemies = new System.Collections.Generic.List<EnemyAgent>();
            for (var i = 0; i < 4; i++)
            {
                var enemyGo = ProceduralCreatureFactory.CreateCreature(creature, palette, false);
                enemyGo.name = $"Rival Tagger {i + 1}";
                enemyGo.transform.position = new Vector3(-10f + i * 6f, 1f, 12f);
                var nav = enemyGo.AddComponent<UnityEngine.AI.NavMeshAgent>();
                nav.speed = 7f;
                nav.angularSpeed = 720f;
                enemies.Add(enemyGo.AddComponent<EnemyAgent>());
            }
            SetPrivate(director, "enemies", enemies);

            var cameraGo = Camera.main != null ? Camera.main.gameObject : new GameObject("Main Camera");
            cameraGo.tag = "MainCamera";
            var cam = cameraGo.GetComponent<Camera>() ?? cameraGo.AddComponent<Camera>();
            cam.fieldOfView = 58f;
            cam.nearClipPlane = 0.05f;
            cam.farClipPlane = 400f;
            var follow = cameraGo.GetComponent<CameraFollow>() ?? cameraGo.AddComponent<CameraFollow>();
            follow.SetTarget(playerGo.transform);
            var light = new GameObject("Commercial Mobile Key Light").AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1.35f;
            light.shadows = LightShadows.Soft;
            light.transform.rotation = Quaternion.Euler(48f, -32f, 0f);
        }

        private void BuildHud()
        {
            var canvasGo = new GameObject("TAG Runtime HUD");
            var canvas = canvasGo.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGo.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasGo.AddComponent<GraphicRaycaster>();
            timerText = CreateText(canvasGo.transform, "Timer", new Vector2(0f, 1f), new Vector2(18f, -18f), "SURVIVE  0.0s", 28, TextAnchor.UpperLeft);
            objectiveText = CreateText(canvasGo.transform, "Objective", new Vector2(0.5f, 0f), new Vector2(0f, 24f), "Swipe/keyboard to run • Space jump • Shift sprint • E dash", 20, TextAnchor.LowerCenter);
            objectiveText.color = new Color(0.85f, 0.95f, 1f, 0.85f);
        }

        private Text CreateText(Transform parent, string name, Vector2 anchor, Vector2 position, string text, int size, TextAnchor alignment)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent, false);
            var rect = go.AddComponent<RectTransform>();
            rect.anchorMin = anchor;
            rect.anchorMax = anchor;
            rect.anchoredPosition = position;
            rect.sizeDelta = new Vector2(760f, 80f);
            var label = go.AddComponent<Text>();
            label.text = text;
            label.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            label.fontSize = size;
            label.alignment = alignment;
            label.color = Color.white;
            return label;
        }

        private void ReadInput()
        {
            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                var centered = touch.position - new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
                move = Vector2.ClampMagnitude(centered / Mathf.Max(1f, Screen.width * 0.25f), 1f);
            }
            sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            jumpQueued |= Input.GetKeyDown(KeyCode.Space);
            dashQueued |= Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.LeftControl);
        }

        private static void SetPrivate(object instance, string fieldName, object value)
        {
            instance.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(instance, value);
        }
    }
}
