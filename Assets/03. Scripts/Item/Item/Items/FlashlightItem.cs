using UnityEngine;

public class FlashlightItem : Item
{
    GameObject lamp;

    public override void ActivateItem()
    {
        if(lamp == null)
        {
            CreateLamp();
        }
        
        LampOn();
    }

    public override void DeactivateItem()
    {
        LampOff();
    }

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
