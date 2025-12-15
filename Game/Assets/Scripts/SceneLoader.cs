using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneLoader: sceneName is empty!");
        }
    }

    public void QuitGame()
    {
        // Для Android это закрытие приложения
        Application.Quit();
    }
}
