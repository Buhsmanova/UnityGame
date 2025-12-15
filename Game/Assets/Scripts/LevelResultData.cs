using UnityEngine;

public static class LevelResultData
{
    // Сцена, куда вернуться к текущему заданию
    public static string RetrySceneName;

    // Сцена, куда перейти дальше после успеха (след. уровень, меню и т.п.)
    public static string NextSceneName;
}
