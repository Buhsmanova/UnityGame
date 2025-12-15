using UnityEngine;
using UnityEngine.SceneManagement;

public class Game2Controller : MonoBehaviour
{
    [Header("Элементы уровня")]
    [SerializeField] private DraggableShadowItem[] draggableItems; // все перетаскиваемые животные

    [Header("Сцены результатов")]
    [SerializeField] private string successSceneName = "Success";
    [SerializeField] private string failSceneName = "Fail";

    [Header("Куда идём после успеха")]
    [Tooltip("Следующая сцена после успешного прохождения (например, Game2_Level2 или LevelMenu)")]
    [SerializeField] private string nextSceneName = "Game2_Level2";

    private int correctlyPlacedCount = 0;

    private void Start()
    {
        correctlyPlacedCount = 0;

        // проинициализируем все draggable, чтобы они знали, куда сообщать результат
        foreach (var item in draggableItems)
        {
            if (item != null)
            {
                item.Init(this);
            }
        }
    }

    // вызывается, когда какой-то элемент правильно поставили на свою тень
    public void OnItemPlacedCorrect(DraggableShadowItem item)
    {
        correctlyPlacedCount++;

        // когда все на своих тенях — победа
        if (correctlyPlacedCount >= draggableItems.Length)
        {
            LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
            LevelResultData.NextSceneName = nextSceneName;

            GameProgress.UnlockLevel(2);
            SceneManager.LoadScene(successSceneName);
        }
    }

    // вызывается при ошибочном соединении
    public void OnItemPlacedIncorrect(DraggableShadowItem item)
    {
        // запоминаем, откуда возвращаться
        LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
        LevelResultData.NextSceneName = SceneManager.GetActiveScene().name;

        SceneManager.LoadScene(failSceneName);
    }
}
