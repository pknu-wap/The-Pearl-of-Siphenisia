using UnityEngine;

public class HandItem : Item
{
    protected QuickSlot quickSlot;

    private void Awake()
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
