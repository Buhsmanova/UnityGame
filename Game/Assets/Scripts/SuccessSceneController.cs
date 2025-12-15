using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessSceneController : MonoBehaviour
{
    [Header("Аудио")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip successClip;

    [Tooltip("Сцена по умолчанию, если NextSceneName не задан (например, LevelMenu)")]
    [SerializeField] private string defaultNextScene = "LevelMenu";

    [Tooltip("Сцена по умолчанию для возврата, если RetrySceneName не задан")]
    [SerializeField] private string defaultRetryScene = "Game1_Level1";

    [Tooltip("Имя сцены меню уровней или главного меню")]
    [SerializeField] private string menuSceneName = "LevelMenu";

    private void Start()
    {
        PlaySuccessSound();
    }

    private void PlaySuccessSound()
    {
        if (audioSource != null && successClip != null)
        {
            audioSource.clip = successClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("SuccessSceneController: audioSource или successClip не назначены!");
        }
    }

    public void OnRetryButton()
    {
        string retryScene = !string.IsNullOrEmpty(LevelResultData.RetrySceneName)
            ? LevelResultData.RetrySceneName
            : defaultRetryScene;

        SceneManager.LoadScene(retryScene);
    }

    public void OnNextButton()
    {
        string nextScene = !string.IsNullOrEmpty(LevelResultData.NextSceneName)
            ? LevelResultData.NextSceneName
            : defaultNextScene;

        SceneManager.LoadScene(nextScene);
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
