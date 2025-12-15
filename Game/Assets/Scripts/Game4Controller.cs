using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game4Controller : MonoBehaviour
{
    [Header("Обводки отличий")]
    [Tooltip("Обводки на ЛЕВОЙ картинке, в порядке: отличие 0, 1, 2...")]
    [SerializeField] private GameObject[] leftOutlines;

    [Tooltip("Обводки на ПРАВОЙ картинке, в том же порядке")]
    [SerializeField] private GameObject[] rightOutlines;

    [Header("Сцены результатов")]
    [SerializeField] private string successSceneName = "Success";
    [SerializeField] private string failSceneName = "Fail";

    [Header("Куда идём после успеха")]
    [Tooltip("Следующая сцена после успешного прохождения (например, Game4_Level2 или LevelMenu)")]
    [SerializeField] private string nextSceneName = "Game4_Level2";

    [Header("Какой глобальный уровень разблокировать при победе")]
    [Tooltip("0 = ничего не разблокировать. Например, на Game4_Level2 поставить 5, чтобы открыть 5-й уровень в меню.")]
    [SerializeField] private int unlockLevelOnWin = 5;

    private bool[] found;   // какие отличия уже нашлись
    private int foundCount = 0;

    private void Start()
    {
        int count = leftOutlines.Length;
        found = new bool[count];
        foundCount = 0;

        // выключаем все обводки на старте
        for (int i = 0; i < count; i++)
        {
            if (leftOutlines[i] != null) leftOutlines[i].SetActive(false);
            if (rightOutlines[i] != null) rightOutlines[i].SetActive(false);
            found[i] = false;
        }
    }

    // вызывается кнопками отличий (и слева, и справа)
    public void OnDifferenceClicked(int index)
    {
        // защитимся от ошибок
        if (index < 0 || index >= found.Length)
        {
            Debug.LogWarning("Game4Controller: неправильный index " + index);
            return;
        }

        // если уже нашли это отличие — ничего не делаем
        if (found[index]) return;

        found[index] = true;
        foundCount++;

        // включаем обводки на обеих картинках
        if (leftOutlines[index] != null) leftOutlines[index].SetActive(true);
        if (rightOutlines[index] != null) rightOutlines[index].SetActive(true);

        // если нашли все отличия -> победа
        if (foundCount >= found.Length)
        {
            if (unlockLevelOnWin > 0)
            {
                GameProgress.UnlockLevel(unlockLevelOnWin);
            }

            LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
            LevelResultData.NextSceneName = nextSceneName;

            SceneManager.LoadScene(successSceneName);
        }

    }

    // вызывается при клике в пустое место
    public void OnWrongClick()
    {
        LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
        LevelResultData.NextSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(failSceneName);
    }
}
