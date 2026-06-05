using TAG.Save;

namespace TAG.Progression
{
    public sealed class ProgressionService
    {
        public int XpForNextLevel(int level) => 150 + level * 75;

        public void AddXp(PlayerSaveData save, int amount)
        {
            save.xp += amount;
            while (save.xp >= XpForNextLevel(save.accountLevel))
            {
                save.xp -= XpForNextLevel(save.accountLevel);
                save.accountLevel++;
            }
        }
    }
}
