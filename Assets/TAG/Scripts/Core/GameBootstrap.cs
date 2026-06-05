using TAG.Content;
using TAG.Save;
using TAG.Services;
using UnityEngine;

namespace TAG.Core
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private GameDataCatalog catalog;
        [SerializeField] private bool prefer120Fps;

        public static GameBootstrap Instance { get; private set; }
        public GameDataCatalog Catalog => catalog;
        public PlayerSaveData SaveData { get; private set; }
        public SaveSystem SaveSystem { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = prefer120Fps ? GameConstants.PremiumFrameRate : GameConstants.TargetFrameRate;
            QualitySettings.vSyncCount = 0;
            if (catalog == null)
            {
                catalog = Resources.Load<GameDataCatalog>("TAG/GameDataCatalog") ?? RuntimeDefaultContent.Catalog();
            }
            SaveSystem = new SaveSystem();
            SaveData = SaveSystem.Load();
            GetComponent<CloudSaveService>()?.TryHydrate(SaveData);
            GetComponent<AnalyticsService>()?.Track("bootstrap_ready");
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveSystem?.Save(SaveData);
                GetComponent<CloudSaveService>()?.QueueUpload(SaveData);
            }
        }

        private void OnApplicationQuit()
        {
            SaveSystem?.Save(SaveData);
            GetComponent<CloudSaveService>()?.QueueUpload(SaveData);
        }
    }
}
