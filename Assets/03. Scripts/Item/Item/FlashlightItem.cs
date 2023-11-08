using UnityEngine;

public class FlashlightItem : Item
{
    public override void UseItem()
    {
        // 손전등을 플레이어에게 추가한다.
        Debug.Log("손전등 +1");
    }
}
