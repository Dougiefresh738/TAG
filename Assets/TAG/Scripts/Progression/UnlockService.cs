using System.Linq;
using TAG.Core;
using TAG.Maps;
using TAG.Save;

namespace TAG.Progression
{
    public sealed class UnlockService
    {
        public bool IsMapUnlocked(PlayerSaveData save, MapDefinition map)
        {
            return map != null && save.accountLevel >= map.unlockLevel;
        }

        public bool IsDifficultyUnlocked(PlayerSaveData save, DifficultyTier tier)
        {
            if (tier == DifficultyTier.GodMode)
            {
                return save.accountLevel >= GameConstants.GodModeUnlockLevel
                       && save.maps.Count > 0
                       && save.maps.All(m => m.completed)
                       && save.maps.All(m => m.stars.Count >= 6 && m.stars.All(s => s.stars >= 3));
            }

            return save.accountLevel >= GameConstants.DifficultyUnlockLevel;
        }

        public float MapUnlockProgress(PlayerSaveData save, MapDefinition map)
        {
            if (map == null || map.unlockLevel <= 1) return 1f;
            return UnityEngine.Mathf.Clamp01(save.accountLevel / (float)map.unlockLevel);
        }
    }
}
