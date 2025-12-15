using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroTravelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text introText;
    [SerializeField] private Image characterImage;

    [Header("Спрайты персонажей")]
    [SerializeField] private Sprite tigerSprite;
    [SerializeField] private Sprite bearSprite;

    private void Start()
    {
        switch (CharacterSelection.SelectedCharacter)
        {
            case CharacterType.Tiger:
                introText.text = "Ты отправляешься в веселое путешествие с тигрёнком Тайгой!";
                characterImage.sprite = tigerSprite;
                break;

            case CharacterType.Bear:
                introText.text = "Ты отправляешься в веселое путешествие с медвежонком Топтыжкой!";
                characterImage.sprite = bearSprite;
                break;
        }
    }
}
