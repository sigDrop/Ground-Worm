using UnityEngine;

public static class LevelsProgress
{
    public static int GetLastUnlockedLevel()
    {
        return PlayerPrefs.GetInt("UnlockedLevel", 0);
    }

    public static void UnlockLevel(int levelIndex)
    {
        int currentUnlocked = GetLastUnlockedLevel();

        if (levelIndex > currentUnlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", levelIndex);
            PlayerPrefs.Save();
        }
    }
}
