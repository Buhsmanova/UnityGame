using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableShadowItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Id пары")]
    [SerializeField] private int id; // например: 0 = тигр, 1 = медведь и т.п.

    [Header("Ссылки")]
    [SerializeField] private Canvas canvas; // Canvas, внутри которого двигаем

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startAnchoredPos;
    private Game2Controller controller;

    private bool isPlacedOnTarget = false;

    public int Id => id;

    public void Init(Game2Controller controller)
    {
        this.controller = controller;
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    private void Start()
    {
        startAnchoredPos = rectTransform.anchoredPosition;

        if (canvas == null)
        {
            canvas = GetComponentInParent<Canvas>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isPlacedOnTarget = false;
        // чтобы raycast доходил до целей под объектом
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // возвращаем блокировку raycast
        canvasGroup.blocksRaycasts = true;

        // если цель нас не приняла (PlaceOnTarget не вызывался или был неверный таргет) — возвращаем на место
        if (!isPlacedOnTarget)
        {
            rectTransform.anchoredPosition = startAnchoredPos;
        }
    }

    // Вызывается ShadowTarget, когда на него уронили этот объект
    public void PlaceOnTarget(ShadowTarget target, bool isCorrect)
    {
        if (isCorrect)
        {
            isPlacedOnTarget = true;
            rectTransform.anchoredPosition = ((RectTransform)target.transform).anchoredPosition;

            controller?.OnItemPlacedCorrect(this);
        }
        else
        {
            // неверный таргет — откат на старт и сообщаем про ошибку
            isPlacedOnTarget = false;
            rectTransform.anchoredPosition = startAnchoredPos;

            controller?.OnItemPlacedIncorrect(this);
        }
    }
}
