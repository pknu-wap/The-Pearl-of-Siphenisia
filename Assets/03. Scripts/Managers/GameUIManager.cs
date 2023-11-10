using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    private GameObject inventoryUIObj;
    private GameObject interactionUIObj;
    public Vector3 interactOffset = new(0f, 1f, 0f);

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
        CloseInteractionUI();
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
        inventoryUIObj = GameObject.Find("Inventory");
        interactionUIObj = GameObject.Find("Interaction Button");
    }

    #region Inventory
    public void SwitchActiveInventoryUI()
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

    public void OpenInventoryUI()
    {
        inventoryUIObj.SetActive(true);
    }

    public void CloseInventoryUI()
    {
        inventoryUIObj.SetActive(false);
    }
    #endregion Inventory

    #region Interaction UI
    public void FloatInteractionUI(Transform targetTransform)
    {
        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
        interactionUIObj.SetActive(true);
    }

    public void CloseInteractionUI()
    {
        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform transform)
    {

    }
    #endregion Interaction UI
}
