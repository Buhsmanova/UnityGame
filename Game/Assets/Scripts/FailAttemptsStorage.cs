using UnityEngine;

/// <summary>
/// Хранит и изменяет число неудачных попыток через PlayerPrefs.
/// Не вешать на объекты (не MonoBehaviour).
/// </summary>
public static class FailAttemptsStorage
{
    private const string Key = "FailAttemptsCount";

    public static int Count => PlayerPrefs.GetInt(Key, 0);

    public static void AddFail()
    {
        PlayerPrefs.SetInt(Key, Count + 1);
        PlayerPrefs.Save();
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteKey(Key);
    }
}
