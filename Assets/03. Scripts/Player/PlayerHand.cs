using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    private Item currentItem;

    private void Awake()
    {

    }

    public void HandItem(Item item)
    {
        currentItem = item;
    }
}
