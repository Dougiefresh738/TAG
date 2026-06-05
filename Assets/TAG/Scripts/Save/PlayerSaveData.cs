using System;
using System.Collections.Generic;
using TAG.Progression;

namespace TAG.Save
{
    [Serializable]
    public sealed class PlayerSaveData
    {
        public int accountLevel = 1;
        public int xp;
        public string selectedCreatureId = "forest_hopper";
        public DifficultyTier selectedDifficulty = DifficultyTier.Easy;
        public List<string> unlockedCreatureIds = new() { "forest_hopper" };
        public List<string> unlockedSkinIds = new();
        public List<string> unlockedTitleIds = new();
        public List<string> unlockedBadgeIds = new();
        public List<string> claimedLoginRewardIds = new();
        public List<MapProgress> maps = new();
        public List<ChallengeProgress> dailyChallenges = new();
        public List<ChallengeProgress> weeklyChallenges = new();
        public List<string> earnedAchievementIds = new();
        public BattlePassProgress battlePass = new();
    }

    [Serializable]
    public sealed class MapProgress
    {
        public string mapId;
        public bool completed;
        public List<DifficultyStars> stars = new();
    }

    [Serializable]
    public sealed class DifficultyStars
    {
        public DifficultyTier difficulty;
        public int stars;
    }

    [Serializable]
    public sealed class ChallengeProgress
    {
        public string challengeId;
        public int current;
        public int target;
        public bool claimed;
    }

    [Serializable]
    public sealed class BattlePassProgress
    {
        public int seasonNumber = 1;
        public int tier;
        public int seasonXp;
        public bool premiumOwned;
    }
}
