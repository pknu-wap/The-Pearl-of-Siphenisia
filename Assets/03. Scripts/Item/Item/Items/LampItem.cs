using UnityEngine;

public class LampItem : Item
{
    GameObject lamp;
    PlayerCollision playerCollision;

    private void Awake()
    {
        playerCollision = GameObject.FindWithTag("Player").GetComponent<PlayerCollision>();
        lamp = playerCollision.transform.GetChild(1).gameObject;
    }

    public override bool ActivateItem()
    {
        if(playerCollision.currentLamp != null)
        {
            return false;
        }
        
        LampOn();
        playerCollision.currentLamp = this;

        return true;
    }

    public override bool DeactivateItem()
    {
        if (playerCollision.currentLamp == null)
        {
            return false;
        }

        LampOff();
        playerCollision.currentLamp = null;

        return true;
    }

    // Legacy
    public void CreateLamp()
    {
        GameObject obj = (GameObject)Resources.Load("Prefebs/Lamp");
        lamp = Instantiate(obj, GameObject.FindWithTag("Player").transform);
    }

    public void SetLampData()
    {

    }

    public void LampOn()
    {
        lamp.SetActive(true);
    }

    public void LampOff()
    {
        lamp.SetActive(false);
    }
}
