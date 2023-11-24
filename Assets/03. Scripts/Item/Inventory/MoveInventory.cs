using UnityEngine;
using UnityEngine.EventSystems;

// https://rito15.github.io/posts/unity-study-rpg-inventory/
public class MoveInventory : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform targetTr;

    public Vector2 beginPoint;
    public Vector2 moveBegin;

    private void Awake()
    {
        if(targetTr == null)
        {
            targetTr = transform.parent;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        beginPoint = targetTr.position;
        moveBegin = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        targetTr.position = beginPoint + (eventData.position - moveBegin);
    }

}
