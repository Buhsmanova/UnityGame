using UnityEngine;
using TMPro; // Замените на UnityEngine.UI, если используете обычный Text.

/// <summary>
/// Вешается на текст в финальной сцене, выводит число попыток.
/// </summary>
public class FailAttemptsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private string prefix = "Всего попыток: ";

    private void Awake()
    {
        if (targetText == null)
            targetText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (targetText != null)
            targetText.text = prefix + FailAttemptsStorage.Count;
    }
}
