using TAG.Save;
using UnityEngine;

namespace TAG.Services
{
    public sealed class CloudSaveService : MonoBehaviour
    {
        [SerializeField] private bool cloudSaveEnabled = true;
        [SerializeField] private string provider = "Android/Unity Cloud Save Adapter";

        public bool CloudSaveEnabled => cloudSaveEnabled;
        public string Provider => provider;

        public void QueueUpload(PlayerSaveData data)
        {
            if (!cloudSaveEnabled || data == null) return;
            PlayerPrefs.SetString("tag_cloud_save_shadow", JsonUtility.ToJson(data));
            PlayerPrefs.SetString("tag_cloud_save_provider", provider);
        }

        public bool TryHydrate(PlayerSaveData data)
        {
            if (!cloudSaveEnabled || data == null || !PlayerPrefs.HasKey("tag_cloud_save_shadow")) return false;
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("tag_cloud_save_shadow"), data);
            return true;
        }
    }
}
