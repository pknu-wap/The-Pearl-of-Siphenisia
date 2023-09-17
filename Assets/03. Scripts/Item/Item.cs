using UnityEngine;

public class Item : ScriptableObject
{
    public Sprite _icon;
    public Sprite _sprite;
    public string _name;
    public string _description;

    public Sprite Icon { get; set; }
    public Sprite Sprite { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
