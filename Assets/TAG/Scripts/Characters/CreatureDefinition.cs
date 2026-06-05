using System.Collections.Generic;
using UnityEngine;

namespace TAG.Characters
{
    [CreateAssetMenu(menuName = "TAG/Characters/Creature Definition")]
    public sealed class CreatureDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string creatureId = "forest_hopper";
        public string displayName = "Forest Hopper";
        public RarityTier rarity = RarityTier.Common;
        [TextArea] public string personality = "Cheery, bouncy, and impossible not to love.";

        [Header("Collectible Presentation")]
        public Sprite portrait;
        public GameObject playablePrefab;
        public List<string> unlockableSkinIds = new();
        public List<string> emoteIds = new();
        public AnimationClip idleAnimation;
        public AnimationClip victoryAnimation;
        public AnimationClip unlockAnimation;

        [Header("Movement Tuning")]
        public float moveSpeed = 8f;
        public float sprintMultiplier = 1.35f;
        public float dashForce = 16f;
        public float jumpForce = 8f;
        public float grapplePullForce = 22f;
    }
}
