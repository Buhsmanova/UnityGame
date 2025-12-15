using UnityEngine;

public static class GameProgress
{
    // Номер максимального открытого уровня (1 = открыт только Level1)
    public static int MaxUnlockedLevel = 1;

    // Можно сделать метод для безопасного обновления
    public static void UnlockLevel(int levelNumber)
    {
        if (levelNumber > MaxUnlockedLevel)
        {
            MaxUnlockedLevel = levelNumber;
            Debug.Log($"GameProgress: открыт уровень {MaxUnlockedLevel}");
        }
    }
}
