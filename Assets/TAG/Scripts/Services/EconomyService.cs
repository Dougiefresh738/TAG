using TAG.Save;
using UnityEngine;

namespace TAG.Services
{
    public sealed class EconomyService : MonoBehaviour
    {
        [SerializeField] private bool premiumPurchasesEnabled = true;

        public bool CanPurchaseCosmetic(string rewardId) => premiumPurchasesEnabled && !string.IsNullOrWhiteSpace(rewardId);

        public void GrantCosmetic(PlayerSaveData save, string rewardId)
        {
            if (save == null || string.IsNullOrWhiteSpace(rewardId) || save.unlockedSkinIds.Contains(rewardId)) return;
            save.unlockedSkinIds.Add(rewardId);
        }

        public bool TryGrantBattlePassReward(PlayerSaveData save, string freeRewardId, string premiumRewardId)
        {
            if (save == null) return false;
            GrantCosmetic(save, freeRewardId);
            if (save.battlePass.premiumOwned) GrantCosmetic(save, premiumRewardId);
            return true;
        }
    }
}
