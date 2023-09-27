using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    GameObject inventoryObj;

    private void Awake()
    {
        inventoryObj = GameObject.Find("Inventory");
    }
}
