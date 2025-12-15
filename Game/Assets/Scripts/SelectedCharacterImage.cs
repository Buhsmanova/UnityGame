using UnityEngine;
using UnityEngine.UI;

public class SelectedCharacterImage : MonoBehaviour
{
    [SerializeField] private Image characterImage;

    [Header("Спрайты персонажей")]
    [SerializeField] private Sprite tigerSprite;
    [SerializeField] private Sprite bearSprite;

    private void Awake()
    {
        // если забыли привязать вручную — берём Image с текущего объекта
        if (characterImage == null)
            characterImage = GetComponent<Image>();
    }

    private void Start()
    {
        // читаем выбранного персонажа
        switch (CharacterSelection.SelectedCharacter)
        {
            case CharacterType.Bear:
                characterImage.sprite = bearSprite;
                break;

            case CharacterType.Tiger:
            default:
                characterImage.sprite = tigerSprite;
                break;
        }
    }
}
