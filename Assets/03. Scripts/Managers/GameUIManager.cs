using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    private GameObject inventoryUIObj;
    private GameObject interactionUIObj;
    private Vector3 interactOffset = new(0f, 1f, 0f);

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
        HideInventoryUI();
        HideInteractionUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
    }

    private void AssignObjects()
    {
        inventoryUIObj = GameObject.Find("Inventory");
        interactionUIObj = GameObject.Find("Interaction Button");
    }

    #region Inventory
    // 인벤토리 UI가 켜져 있으면 끄고, 꺼져 있으면 켜는 함수
    public void ToggleInventoryUI()
    {
        if (inventoryUIObj.activeSelf == true)
        {
            inventoryUIObj.SetActive(false);
        }

        else
        {
            inventoryUIObj.SetActive(true);
        }
    }

    public void ShowInventoryUI()
    {
        inventoryUIObj.SetActive(true);
    }

    public void HideInventoryUI()
    {
        inventoryUIObj.SetActive(false);
    }
    #endregion Inventory

    #region Interaction UI
    public void ShowInteractionUI(Transform targetTransform)
    {
        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
        interactionUIObj.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform transform)
    {

    }
    #endregion Interaction UI
}
