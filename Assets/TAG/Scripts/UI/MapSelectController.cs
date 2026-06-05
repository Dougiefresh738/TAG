using System.Collections.Generic;
using TAG.Core;
using TAG.Maps;
using TAG.Progression;
using TAG.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TAG.UI
{
    public sealed class MapSelectController : MonoBehaviour
    {
        [SerializeField] private GameDataCatalog catalog;
        [SerializeField] private Transform cardParent;
        [SerializeField] private MapCardView cardPrefab;
        private readonly UnlockService unlocks = new();

        private void OnEnable()
        {
            Rebuild();
        }

        public void Rebuild()
        {
            if (catalog == null || cardParent == null || cardPrefab == null) return;
            foreach (Transform child in cardParent) Destroy(child.gameObject);
            var save = GameBootstrap.Instance != null ? GameBootstrap.Instance.SaveData : new PlayerSaveData();
            foreach (var map in catalog.maps)
            {
                var card = Instantiate(cardPrefab, cardParent);
                card.Bind(map, unlocks.IsMapUnlocked(save, map), unlocks.MapUnlockProgress(save, map));
            }
        }
    }

    public sealed class MapCardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text requirement;
        [SerializeField] private Image preview;
        [SerializeField] private Slider progress;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject lockedOverlay;

        public void Bind(MapDefinition map, bool unlocked, float unlockProgress)
        {
            if (title != null) title.text = map.displayName;
            if (requirement != null) requirement.text = unlocked ? "Ready to play" : $"Unlocks at Level {map.unlockLevel}";
            if (preview != null) preview.sprite = map.previewImage;
            if (progress != null) progress.value = unlockProgress;
            if (playButton != null) playButton.interactable = unlocked;
            if (lockedOverlay != null) lockedOverlay.SetActive(!unlocked);
        }
    }
}
