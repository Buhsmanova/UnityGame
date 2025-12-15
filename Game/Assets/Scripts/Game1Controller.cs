using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game1Controller : MonoBehaviour
{
    [Header("Настройки задания")]
    [Tooltip("Индекс правильной картинки (0..3)")]
    [SerializeField] private int correctIndex = 0;

    [Tooltip("Кнопки/картинки с вариантами ответа (4 штуки)")]
    [SerializeField] private Button[] optionButtons;

    [Header("Сцены результатов")]
    [Tooltip("Имя сцены с победой (Success)")]
    [SerializeField] private string successSceneName = "Success";

    [Tooltip("Имя сцены с поражением (Fail)")]
    [SerializeField] private string failSceneName = "Fail";

    [Header("Куда идти после успеха")]
    [Tooltip("Имя сцены со следующим уровнем или меню (например, Game1_Level2 или LevelMenu)")]
    [SerializeField] private string nextSceneName = "LevelMenu";

    private bool isAnswered = false;

    private void Start()
    {
        // Подписываемся на клики кнопок
        for (int i = 0; i < optionButtons.Length; i++)
        {
            int index = i; // локальная копия для замыкания
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }
    }

    public void OnOptionSelected(int index)
    {
        if (isAnswered) return;
        isAnswered = true;
        SetOptionsInteractable(false);

        // Запоминаем, откуда пришли и куда идём (если используешь LevelResultData)
        LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
        LevelResultData.NextSceneName = nextSceneName;

        if (index == correctIndex)
        {
            // При победе на 1-м уровне открываем 2-й
            GameProgress.UnlockLevel(2);

            LoadSuccessScene();
        }
        else
        {
            LoadFailScene();
        }
    }


    private void LoadSuccessScene()
    {
        if (!string.IsNullOrEmpty(successSceneName))
        {
            SceneManager.LoadScene(successSceneName);
        }
        else
        {
            Debug.LogWarning("Game1Controller: successSceneName не задан!");
        }
    }

    private void LoadFailScene()
    {
        if (!string.IsNullOrEmpty(failSceneName))
        {
            SceneManager.LoadScene(failSceneName);
        }
        else
        {
            Debug.LogWarning("Game1Controller: failSceneName не задан!");
        }
    }

    private void SetOptionsInteractable(bool value)
    {
        foreach (var btn in optionButtons)
        {
            if (btn != null)
                btn.interactable = value;
        }
    }
}
