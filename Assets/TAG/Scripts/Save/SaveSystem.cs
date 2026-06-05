using System.IO;
using UnityEngine;

namespace TAG.Save
{
    public sealed class SaveSystem
    {
        private const string FileName = "tag_player_save.json";
        private readonly string path;

        public SaveSystem(string overridePath = null)
        {
            path = overridePath ?? Path.Combine(Application.persistentDataPath, FileName);
        }

        public PlayerSaveData Load()
        {
            if (!File.Exists(path))
            {
                return new PlayerSaveData();
            }

            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerSaveData>(json) ?? new PlayerSaveData();
        }

        public void Save(PlayerSaveData data)
        {
            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
        }
    }
}
