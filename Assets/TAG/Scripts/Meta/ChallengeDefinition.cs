using UnityEngine;

namespace TAG.Meta
{
    [CreateAssetMenu(menuName = "TAG/Meta/Challenge Definition")]
    public sealed class ChallengeDefinition : ScriptableObject
    {
        public string challengeId;
        public string displayName;
        public ChallengeCadence cadence;
        public int target;
        public int xpReward;
        public string cosmeticRewardId;
    }

    public enum ChallengeCadence
    {
        Daily,
        Weekly
    }
}
