using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region 변수
    private Image bg;
    public List<Slot>[] slots;
    private ItemInfoWindow infoWindow;
    public GameObject[] inventoryTab;
    private int currentTab = 0;
    #endregion 변수

    #region 초기 설정
    private void Awake()
    {
        AssignObjects();
        // 처음엔 장비 탭을 열어둔다.
        SwitchInventoryTab(0);
    }

    /// <summary>
    /// 변수 할당
    /// </summary>
    void AssignObjects()
    {
        bg = GetComponent<Image>();
        infoWindow = GameObject.Find("ItemInfoWindow").GetComponent<ItemInfoWindow>();
        slots = new List<Slot>[3];
        inventoryTab = new GameObject[3];

        // 인벤토리 탭 할당
        for(int i = 0; i < inventoryTab.Length; i++)
        {
            inventoryTab[i] = transform.GetChild(1).GetChild(i).gameObject;
        }

        // slots 할당 -> 각 탭의 모든 슬롯을 할당한다.
        for (int i = 0; i < slots.Length; i++) {
            slots[i] = new();
            // 자식 오브젝트 slot을 모두 할당
            foreach (Transform slot in transform.GetChild(1).GetChild(i))
            {
                slots[i].Add(slot.GetComponent<Slot>());
            }
        }


    }
    #endregion 초기 설정

    #region 아이템 상호작용
    /// <summary>
    /// 비어 있는 맨 앞 슬롯 인덱스 반환한다. 존재하지 않으면 slots.Coun를t 반환한다.
    /// </summary>
    /// <returns></returns>
    int SearchFirstEmptySlot(int useTag)
    {
        int i = 0;

        // 비어있지 않다면 i를 1 증가
        while (i < slots[useTag].Count && slots[useTag][i].IsEmpty() == false)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// itemData를 갖는 가장 앞 쪽 슬롯 인덱스를 반환한다. 존재하지 않으면 slots.Count를 반환한다.
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    int SearchSlotIndex(ItemData itemData)
    {
        int i = 0;

        // 비어있지 않다면 i를 1 증가
        while (i < slots[(int)itemData.useTag].Count && slots[(int)itemData.useTag][i].slotItem == itemData)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// 비어있는 맨 앞 슬롯에 아이템 추가
    /// </summary>
    /// <param name="itemData"></param>
    public void AddItem(Item item)
    {
        int i;

        // 소비 아이템이라면 이미 존재하는지 검사한다.
        if (item.itemData.useTag == UseTag.Consume)
        {
            i = SearchSlotIndex(item.itemData);

            // 아이템이 존재하지 않으면 빈 슬롯을 찾는다.
            if(i >= slots[(int)item.itemData.useTag].Count)
            {
                i = SearchFirstEmptySlot((int)item.itemData.useTag);
            }
        }
        else
        {
            i = SearchFirstEmptySlot((int)item.itemData.useTag);
        }

        // 슬롯이 모두 찼다면 종료
        if(i == slots[(int)item.itemData.useTag].Count)
        {
            Debug.Log("슬롯이 모두 찼습니다.");
            return;
        }

        slots[(int)item.itemData.useTag][i].AddItem(item);
        slots[(int)item.itemData.useTag][i].UpdateSlotUI();
    }

    /// <summary>
    /// i번 슬롯의 아이템 삭제
    /// </summary>
    /// <param name="i"></param>
    public void RemoveItem(int useTag, int i)
    {
        slots[useTag][i].DecreaseItemCount();
    }
    #endregion 아이템 상호작용

    #region 정렬
    /// <summary>
    /// 현재 탭의 아이템들을 위로 모은다.
    /// </summary>
    public void TrimCurrentTab()
    {
        TrimInventory(currentTab);

        // UI 갱신
        foreach (Slot slot in slots[currentTab])
        {
            slot.UpdateSlotUI();
        }
    }

    /// <summary>
    /// 인벤토리의 공백을 제거하는 함수, 마지막 인덱스를 반환
    /// </summary>
    public int TrimInventory(int index)
    {
        // 첫번째 공백 찾기
        int i = -1;
        while (++i < slots[index].Count && slots[index][i].slotItem != null) ;

        if(i == slots[index].Count)
        {
            // 마지막 슬롯의 번호 반환
            return i - 1;
        }

        int j = i;
        while (true)
        {
            // 공백이 아닌 칸까지 j를 전진시킨다.
            while (++j < slots[index].Count && slots[index][j].slotItem == null) ;

            if (j == slots[index].Count)
            {
                // 마지막 아이템의 번호 반환
                return i - 1;
            }

            // 현재 공백인 i에 다음 아이템을 넣고, i를 한 칸 전진시킨다. 반복
            slots[index][i].slotItem = slots[index][j].slotItem;
            slots[index][j].slotItem = null;
            i++;
        }
    }

    /// <summary>
    /// 현재 탭을 정렬시킨다.
    /// </summary>
    public void SortCurrentTab()
    {
        SortInventory(currentTab);
    }

    /// <summary>
    /// 인벤토리를 정렬(임시로 우선 순위를 부여)
    /// </summary>
    public void SortInventory(int index)
    {
        // 정렬은 모으기를 함께 실행한다. n = 마지막 아이템 위치
        int n = TrimInventory(index);

        // 정렬(좀 고치고 싶지만...)
        List<Item> items = new();

        // 인벤토리의 아이템 복사
        for(int i = 0; i <= n; i++)
        {
            items.Add(slots[index][i].slotItem);
        }

        // 아이템 정렬
        items = items.OrderBy(x => x.itemData.priority).ToList();

        // 슬롯에 정렬된 아이템을 덮어씌운다.
        for(int i = 0; i < slots[index].Count; i++)
        {
            if(i <= n)
            {
                slots[index][i].AddItem(items[i]);
                continue;
            }

            slots[index][i].AddItem(null);
        }

        // UI 갱신
        foreach (Slot slot in slots[index])
        {
            slot.UpdateSlotUI();
        }
    }
    #endregion 정렬

    #region UI
    public void ShowInventoryUI()
    {
        gameObject.SetActive(true);
    }

    public void HideInventoryUI()
    {
        gameObject.SetActive(false);
        infoWindow.HideInfoUI();
    }

    public bool IsInventoryShowed()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// 인벤토리 탭을 바꾼다.
    /// </summary>
    /// <param name="index"></param>
    public void SwitchInventoryTab(int index)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i == index)
            {
                inventoryTab[i].SetActive(true);
                currentTab = i;
            }
            else
            {
                inventoryTab[i].SetActive(false);
            }
        }
    }
    #endregion UI
}