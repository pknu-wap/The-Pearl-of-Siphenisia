using UnityEngine;

public class ItemData : ScriptableObject
{
    [SerializeField]
    private Sprite _icon;
    public Sprite Icon { get; }

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite { get; }

    [SerializeField]
    private string _name;
    public string Name { get; }

    [SerializeField]
    private string _description;
    public string Description { get; }

}
