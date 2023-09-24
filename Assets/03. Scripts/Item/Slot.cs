using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData itemData;
    public Image icon;
    public Sprite iconSp;

    private void Awake()
    {
        AssignObjects();

        // 세이브 파일에서 아이템을 받아온 후

        UpdateSlot();
    }

    /// <summary>
    /// 변수 할당
    /// </summary>
    void AssignObjects()
    {
        // 자식 오브젝트 Slot Item의 이미지
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="item"></param> 
    public void AddItem(ItemData item)
    {
        itemData = item;

        UpdateSlot();
    }

    /// <summary>
    /// 아이템 삭제
    /// </summary>
    public void RemoveItem()
    {
        itemData = null;

        UpdateSlot();
    }

    /// <summary>
    /// 현재 아이템으로 UI 갱신
    /// </summary>
    public void UpdateSlot()
    {
        if(itemData == null)
        {
            icon.enabled = false;
            return;
        }

        // 아이콘을 변경하고 종료
        icon.sprite = itemData.icon;
        // + 주인공 손에 스프라이트 겹치는 로직 생성 예정
        icon.enabled = true;
    }

    /// <summary>
    /// 상세정보 표시
    /// </summary>
    public void FloatInfo()
    {
        // 아이템이 없다면 띄우지 않는다.
        if(itemData == null)
        {
            return;
        }

        // 자신의 위치로 상세정보창을 이동
        InfoWindow.Instance.ChangePosition(transform.position);
        InfoWindow.Instance.UpdateInfo(itemData);
        InfoWindow.Instance.FloatInfoUI();
    }

    /// <summary>
    /// 상세정보 숨김
    /// </summary>
    public void HideInfo()
    {
        // 아이템이 없다면 호출하지 않는다.
        if (itemData == null)
        {
            return;
        }

        InfoWindow.Instance.HideInfoUI();
    }

    /// <summary>
    /// 비어 있는 슬롯이라면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return itemData == null;
    }
}
