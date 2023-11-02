using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    GameObject inventoryObj;

    public void Test()
    {
        Debug.Log("레이캐스트 문제 없음");
    }

    private void Awake()
    {
        AssignObjects();
    }

    private void Start()
    {
        CloseInventoryUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SwitchActiveInventoryUI();
        }
    }

    private void AssignObjects()
    {
        inventoryObj = GameObject.Find("Inventory");
    }

    public void SwitchActiveInventoryUI()
    {
        if (inventoryObj.activeSelf == true)
        {
            inventoryObj.SetActive(false);
        }

        else
        {
            inventoryObj.SetActive(true);
        }
    }

    public void OpenInventoryUI()
    {
        inventoryObj.SetActive(true);
    }

    public void CloseInventoryUI()
    {
        inventoryObj.SetActive(false);
    }
}
