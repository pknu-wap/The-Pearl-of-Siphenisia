using UnityEngine;

public class HandItem : Item
{
    [SerializeField]
    protected QuickSlot quickSlot;

    protected virtual void Awake()
    {
        quickSlot = GameObject.Find("Quick Slot").GetComponent<QuickSlot>();
    }

    protected void DestroyItem()
    {
        quickSlot.currentSlot.ClearSlot();
        quickSlot.ClearQuickSlot();
        Destroy(gameObject);
    }
}
