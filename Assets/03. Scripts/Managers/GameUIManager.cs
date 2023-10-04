using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    GameObject inventoryObj;

    private void Awake()
    {
        inventoryObj = GameObject.Find("Inventory");
    }
}
