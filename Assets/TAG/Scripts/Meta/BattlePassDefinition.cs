using System.Collections.Generic;
using UnityEngine;

namespace TAG.Meta
{
    [CreateAssetMenu(menuName = "TAG/Meta/Battle Pass Definition")]
    public sealed class BattlePassDefinition : ScriptableObject
    {
        public int seasonNumber = 1;
        public string seasonName = "Launch Sprint";
        public List<BattlePassTier> tiers = new();
    }

    [System.Serializable]
    public sealed class BattlePassTier
    {
        public int tier;
        public int xpRequired;
        public string freeRewardId;
        public string premiumCosmeticRewardId;
    }
}
