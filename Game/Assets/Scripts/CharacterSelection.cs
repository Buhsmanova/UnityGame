using UnityEngine;
using UnityEngine.SceneManagement;

public enum CharacterType
{
    Tiger,
    Bear
}

public class CharacterSelection : MonoBehaviour
{
    public static CharacterType SelectedCharacter { get; private set; }

    // вызываем при нажатии на тигрёнка
    public void SelectTiger()
    {
        SelectedCharacter = CharacterType.Tiger;
        SceneManager.LoadScene("IntroTravel"); // название следующей сцены
    }

    // вызываем при нажатии на медвежонка
    public void SelectBear()
    {
        SelectedCharacter = CharacterType.Bear;
        SceneManager.LoadScene("IntroTravel");
    }
}
