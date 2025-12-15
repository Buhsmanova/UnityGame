using UnityEngine;
using UnityEngine.SceneManagement;

public class FailSceneController : MonoBehaviour
{
    [Header("Аудио")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip failClip;

    [Tooltip("Сцена по умолчанию для возврата, если RetrySceneName не задан")]
    [SerializeField] private string defaultRetryScene = "Game1_Level1";

    [Tooltip("Имя сцены меню уровней или главного меню")]
    [SerializeField] private string menuSceneName = "LevelMenu";

    private void Start()
    {
        PlayFailSound();
    }

    private void PlayFailSound()
    {
        if (audioSource != null && failClip != null)
        {
            audioSource.clip = failClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("FailSceneController: audioSource или failClip не назначены!");
        }
    }

    public void OnRetryButton()
    {
        string retryScene = !string.IsNullOrEmpty(LevelResultData.RetrySceneName)
            ? LevelResultData.RetrySceneName
            : defaultRetryScene;

        SceneManager.LoadScene(retryScene);
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
