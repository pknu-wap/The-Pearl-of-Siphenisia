using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public Image bg;
    public List<Slot> slots;

    private void Awake()
    {
        AssignObjects();
    }

    /// <summary>
    /// 변수 할당
    /// </summary>
    void AssignObjects()
    {
        bg = GetComponent<Image>();

        // 자식 오브젝트 slot을 모두 할당
        foreach(Transform slot in transform)
        {
            slots.Add(slot.GetComponent<Slot>());
        }
    }

    /// <summary>
    /// 비어 있는 맨 앞 슬롯 인덱스 반환
    /// </summary>
    /// <returns></returns>
    int SearchFirstEmptySlot()
    {
        int i = 0;

        // 비어있지 않다면 i를 1 증가
        while (i < slots.Count && slots[i].IsEmpty() == false)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// 비어있는 맨 앞 슬롯에 아이템 추가
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemData item)
    {
        int i = SearchFirstEmptySlot();

        // 슬롯이 모두 찼다면 종료
        if(i == slots.Count)
        {
            Debug.Log("슬롯이 모두 찼습니다.");
            return;
        }

        slots[i].AddItem(item);
        slots[i].UpdateSlot();
    }

    /// <summary>
    /// i번 슬롯의 아이템 삭제
    /// </summary>
    /// <param name="i"></param>
    public void RemoveItem(int i)
    {
        slots[i].RemoveItem();
    }
}
