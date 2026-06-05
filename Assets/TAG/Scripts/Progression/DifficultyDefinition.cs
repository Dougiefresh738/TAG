using UnityEngine;

namespace TAG.Progression
{
    [CreateAssetMenu(menuName = "TAG/Progression/Difficulty Definition")]
    public sealed class DifficultyDefinition : ScriptableObject
    {
        public DifficultyTier tier;
        public string displayName;
        public int unlockLevel;
        public float enemySpeedMultiplier = 1f;
        public float enemyVisionMultiplier = 1f;
        public float directorAggression = 1f;
        public bool requiresAllMapsCompleted;
        public bool requiresThreeStarsEverywhere;
    }
}
