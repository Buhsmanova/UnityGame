using UnityEngine;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons; // Level1 = 0, Level2 = 1, Level3 = 2 ...

    private void Start()
    {
        int maxLevel = GameProgress.MaxUnlockedLevel;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelNumber = i + 1;

            if (levelNumber <= maxLevel)
            {
                levelButtons[i].interactable = true;
            }
            else
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
