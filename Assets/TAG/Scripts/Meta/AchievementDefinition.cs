using UnityEngine;

namespace TAG.Meta
{
    [CreateAssetMenu(menuName = "TAG/Meta/Achievement Definition")]
    public sealed class AchievementDefinition : ScriptableObject
    {
        public string achievementId;
        public string displayName;
        [TextArea] public string description;
        public Sprite icon;
        public int xpReward;
        public string badgeRewardId;
    }
}
