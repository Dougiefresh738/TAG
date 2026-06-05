using System.Collections.Generic;
using UnityEngine;

namespace TAG.Meta
{
    [CreateAssetMenu(menuName = "TAG/Meta/Cosmetic Reward Catalog")]
    public sealed class CosmeticRewardCatalog : ScriptableObject
    {
        public List<string> skinRewardIds = new();
        public List<string> emoteRewardIds = new();
        public List<string> titleRewardIds = new();
        public List<string> badgeRewardIds = new();
        public List<string> loginRewardIds = new();
    }
}
