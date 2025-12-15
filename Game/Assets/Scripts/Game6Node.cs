using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game6Node : MonoBehaviour
{
    [Header("Пара")]
    [Tooltip("Id пары. У двух одинаковых картинок должен быть один и тот же id.")]
    [SerializeField] private int id;

    [Header("Анимация увеличения")]
    [Tooltip("Во сколько раз увеличиваем выбранную картинку.")]
    [SerializeField] private float selectedScale = 1.15f;

    [Tooltip("Скорость анимации (чем больше, тем быстрее).")]
    [SerializeField] private float scaleSpeed = 10f;

    private Game6LinesController controller;
    private bool isConnected = false;
    private bool isSelected = false;

    private Vector3 initialScale;
    private Coroutine scaleRoutine;

    public int Id => id;
    public bool IsConnected => isConnected;

    public void SetConnected(bool value)
    {
        isConnected = value;
    }

    private void Awake()
    {
        initialScale = transform.localScale;
    }

    public void Init(Game6LinesController c)
    {
        controller = c;
        isConnected = false;
        ResetScaleImmediate();
    }

    // вызывается из Button.OnClick
    public void OnClick()
    {
        if (controller != null)
        {
            controller.OnNodeClicked(this);
        }
    }

    public void SetSelected(bool value)
    {
        if (isSelected == value) return;

        isSelected = value;

        float targetMul = value ? selectedScale : 1f;

        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        scaleRoutine = StartCoroutine(ScaleTo(initialScale * targetMul));
    }

    public void ResetScaleImmediate()
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        transform.localScale = initialScale;
        isSelected = false;
    }

    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 start = transform.localScale;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(start, targetScale, t);
            yield return null;
        }

        transform.localScale = targetScale;
        scaleRoutine = null;
    }
}
