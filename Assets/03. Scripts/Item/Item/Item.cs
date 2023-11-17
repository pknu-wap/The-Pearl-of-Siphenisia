using UnityEngine;

public class Item : MonoBehaviour
{
    // 변하지 않는 정보들
    public ItemData itemData;
    // 변하는 정보들 (각 아이템마다 적용)
    public int count = 1;
    public bool isEquiped = false;

    /// <summary>
    /// 아이템을 사용한다.
    /// </summary>
    public virtual void ActivateItem()
    {

    }


    /// <summary>
    /// 아이템을 해제한다. (Equip, Hand에서 사용)
    /// </summary>
    public virtual void DeactivateItem()
    {

    }
}