using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public Image bg;
    public List<GameObject> slots;

    private void Awake()
    {
        AssignObjects();
    }

    void AssignObjects()
    {
        bg = GetComponent<Image>();

        foreach(Transform slot in transform)
        {
            slots.Add(slot.gameObject);
        }
    }
}
