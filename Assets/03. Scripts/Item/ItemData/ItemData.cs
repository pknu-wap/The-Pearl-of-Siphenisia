using UnityEngine;
using UnityEngine.Events;

public class ItemData : ScriptableObject
{
    public Sprite icon;
    public Sprite sprite;

    // name이 이미 있다...
    public string itemName;
    public string description;

    // 정렬 우선 순위
    public int priority;

    public UnityEvent useItemEvent;
}
