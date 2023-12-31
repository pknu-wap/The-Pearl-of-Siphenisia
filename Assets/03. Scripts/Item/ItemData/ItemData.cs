using UnityEngine;

#region 태그
public enum PurposeTag
{
    Puzzle = 0,
    Explore = 1
}

public enum UseTag
{
    Equip = 0,
    Hand = 1,
    Consume = 2
}
#endregion 태그

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("스프라이트")]
    public Sprite icon = null;
    public Sprite sprite = null;

    [Header("아이템 정보")]
    // name이 이미 있다.
    public string itemName = null;
    public string description = null;
    public int count = 1;
    // 정렬 우선 순위
    public int priority = 0;
    public string itemCode = "영어 이름 작성";

    [Header("아이템 분류")]
    public PurposeTag purposeTag;
    public UseTag useTag;
}
