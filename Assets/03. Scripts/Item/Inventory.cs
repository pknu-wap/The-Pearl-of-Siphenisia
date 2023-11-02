using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
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
        foreach(Transform slot in transform.GetChild(1).transform)
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

    /// <summary>
    /// 인벤토리의 공백을 제거하는 함수, 마지막 인덱스를 반환
    /// </summary>
    public int TrimInventory()
    {
        // 빈 칸 없애기
        int i = -1;
        while (++i < slots.Count && slots[i].itemData != null) ;

        if(i == slots.Count)
        {
            return i - 1;
        }

        int j = i;

        while (true)
        {
            while (++j < slots.Count && slots[j].itemData == null) ;

            if (j == slots.Count)
            {
                return i - 1;
            }

            slots[i].itemData = slots[j].itemData;
            slots[j].itemData = null;
            i++;
        }
    }

    /// <summary>
    /// 인벤토리를 정렬(임시로 우선 순위를 부여)
    /// </summary>
    public void SortInventory()
    {
        int n = TrimInventory();

        // 정렬(좀 고치고 싶지만...)
        List<ItemData> items = new List<ItemData>();

        for(int i = 0; i <= n; i++)
        {
            items.Add(slots[i].itemData);
        }

        items = items.OrderBy(x => x.priority).ToList();

        for(int i = 0; i < slots.Count; i++)
        {
            if(i > n)
            {
                slots[i].AddItem(null);
                continue;
            }

            slots[i].AddItem(items[i]);
        }

        // UI 갱신
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }

    /// <summary>
    /// 이벤트 등록용 임시 함수
    /// </summary>
    public void Trim()
    {
        int i = TrimInventory();

        // UI 갱신
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }
}