using UnityEngine;

public enum ItemTag
{
    Clue = 0,
    Explore = 1
};

public class ItemData : ScriptableObject
{
    public Sprite icon = null;
    public Sprite sprite = null;

    // name이 이미 있다.
    public string itemName = null;
    public string description = null;

    // 정렬 우선 순위
    public int priority = 0;

    public ItemTag tag;
}
