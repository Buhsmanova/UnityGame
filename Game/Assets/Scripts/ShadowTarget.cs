using UnityEngine;
using UnityEngine.EventSystems;

public class ShadowTarget : MonoBehaviour, IDropHandler
{
    [SerializeField] private int id; // тот же id, что и у правильного DraggableShadowItem

    public int Id => id;

    public void OnDrop(PointerEventData eventData)
    {
        var draggedObj = eventData.pointerDrag;
        if (draggedObj == null) return;

        var draggable = draggedObj.GetComponent<DraggableShadowItem>();
        if (draggable == null) return;

        bool isCorrect = draggable.Id == id;

        draggable.PlaceOnTarget(this, isCorrect);
    }
}
