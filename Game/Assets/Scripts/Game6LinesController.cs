using UnityEngine;
using UnityEngine.SceneManagement;

public class Game6LinesController : MonoBehaviour
{
    [Header("Точки (картинки) для соединения")]
    [Tooltip("Все картинки, которые нужно соединять попарно. Кол-во = 2 * кол-во пар.")]
    [SerializeField] private Game6Node[] nodes;

    [Header("Префаб линии")]
    [Tooltip("Префаб линии (UI Image) — тонкая полоска.")]
    [SerializeField] private RectTransform linePrefab;

    [Tooltip("Родитель для линий (Canvas или отдельный RectTransform).")]
    [SerializeField] private RectTransform linesParent;

    [Header("Сцены результатов")]
    [SerializeField] private string successSceneName = "Success";
    [SerializeField] private string failSceneName = "Fail";

    [Header("Куда идём после успеха")]
    [Tooltip("Следующая сцена после успешного прохождения (например, Game6_Level2 или LevelMenu).")]
    [SerializeField] private string nextSceneName = "Game6_Level2";

    [Header("Какой глобальный уровень разблокировать при победе")]
    [Tooltip("0 = ничего не разблокировать. Например, на Game6_Level2 поставить 7.")]
    [SerializeField] private int unlockLevelOnWin = 0;

    private Game6Node firstSelected;
    private int correctConnections = 0;

    private void Start()
    {
        foreach (var node in nodes)
        {
            if (node != null)
                node.Init(this);
        }

        firstSelected = null;
        correctConnections = 0;
    }

    public void OnNodeClicked(Game6Node node)
    {
        // Если уже в паре — игнорируем
        if (node.IsConnected)
            return;

        // Если ещё ничего не выбрано
        if (firstSelected == null)
        {
            firstSelected = node;
            firstSelected.SetSelected(true);
            return;
        }

        // Клик по той же самой — снять выделение
        if (firstSelected == node)
        {
            firstSelected.SetSelected(false);
            firstSelected = null;
            return;
        }

        // Есть первая и вторая — проверяем пару
        bool isCorrectPair = firstSelected.Id == node.Id;

        if (isCorrectPair)
        {
            // Правильная пара
            CreateLineBetween(firstSelected, node);

            firstSelected.SetSelected(false);
            node.SetSelected(false);

            firstSelected.SetConnected(true);
            node.SetConnected(true);

            firstSelected = null;

            correctConnections++;

            int totalPairs = nodes.Length / 2;
            if (correctConnections >= totalPairs)
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
        else
        {
            // Неправильная пара → снимаем выделение и выкидываем в Fail
            firstSelected.SetSelected(false);
            node.SetSelected(false);
            firstSelected = null;

            LevelResultData.RetrySceneName = SceneManager.GetActiveScene().name;
            LevelResultData.NextSceneName = SceneManager.GetActiveScene().name;

            SceneManager.LoadScene(failSceneName);
        }
    }

    private void CreateLineBetween(Game6Node a, Game6Node b)
    {
        if (linePrefab == null || linesParent == null)
        {
            Debug.LogWarning("Game6LinesController: не задан linePrefab или linesParent");
            return;
        }

        RectTransform line = Instantiate(linePrefab, linesParent);

        RectTransform rectA = a.transform as RectTransform;
        RectTransform rectB = b.transform as RectTransform;

        if (rectA == null || rectB == null)
        {
            Debug.LogWarning("Game6LinesController: один из узлов не RectTransform");
            return;
        }

        Vector2 posA = rectA.anchoredPosition;
        Vector2 posB = rectB.anchoredPosition;

        Vector2 center = (posA + posB) / 2f;
        Vector2 diff = posB - posA;

        float length = diff.magnitude;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        line.anchoredPosition = center;
        line.sizeDelta = new Vector2(length, line.sizeDelta.y);
        line.rotation = Quaternion.Euler(0, 0, angle);
    }
}
