using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuHeroMover : MonoBehaviour
{
    [SerializeField] private RectTransform heroImage;
    [SerializeField] private Button[] levelButtons; // Level1 = 0, Level2 = 1 ...
    [SerializeField] private float moveDuration = 0.4f;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private Vector3 heroOffset = Vector3.zero;

    private Coroutine moveRoutine;

    private void Awake()
    {
        if (heroImage == null)
            heroImage = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (!IsSetupValid())
            return;

        // Step onto the latest unlocked level as soon as the menu opens.
        MoveToLevel(GameProgress.MaxUnlockedLevel, true);
    }

    public void MoveToLevel(int levelNumber, bool animate = true)
    {
        if (!IsSetupValid())
            return;

        int index = Mathf.Clamp(levelNumber - 1, 0, levelButtons.Length - 1);
        RectTransform target = levelButtons[index].GetComponent<RectTransform>();

        MoveToTarget(target, animate);
    }

    public void MoveToButton(Button button, bool animate = true)
    {
        if (button == null)
            return;

        MoveToTarget(button.GetComponent<RectTransform>(), animate);
    }

    private void MoveToTarget(RectTransform target, bool animate)
    {
        if (target == null || heroImage == null)
            return;

        if (moveRoutine != null)
            StopCoroutine(moveRoutine);

        if (!animate || moveDuration <= 0f)
        {
            heroImage.position = target.position + heroOffset;
            moveRoutine = null;
            return;
        }

        moveRoutine = StartCoroutine(MoveRoutine(target.position + heroOffset));
    }

    private IEnumerator MoveRoutine(Vector3 targetPosition)
    {
        Vector3 startPos = heroImage.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = moveCurve.Evaluate(Mathf.Clamp01(elapsed / moveDuration));
            heroImage.position = Vector3.Lerp(startPos, targetPosition, t);
            yield return null;
        }

        heroImage.position = targetPosition;
        moveRoutine = null;
    }

    private bool IsSetupValid()
    {
        return heroImage != null && levelButtons != null && levelButtons.Length > 0;
    }
}
