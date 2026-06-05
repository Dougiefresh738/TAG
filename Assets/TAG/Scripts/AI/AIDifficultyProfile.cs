using UnityEngine;

namespace TAG.AI
{
    [CreateAssetMenu(menuName = "TAG/AI/AI Difficulty Profile")]
    public sealed class AIDifficultyProfile : ScriptableObject
    {
        public float patrolSpeed = 4f;
        public float chaseSpeed = 8f;
        public float visionDistance = 22f;
        public float reactionSeconds = 0.35f;
        public float flankWeight = 0.5f;
        public float ambushWeight = 0.25f;
        public float searchDuration = 8f;
        public bool enableGodModePrediction;
    }
}
