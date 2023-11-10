using UnityEngine;

public class LampSetter : MonoBehaviour
{
    public void CreateLamp()
    {
        Instantiate(this, GameObject.FindWithTag("Player").transform);
    }

    public void SetLampData()
    {

    }

    public void LampOn()
    {
        gameObject.SetActive(true);
    }
    public void LampOff()
    {
        gameObject.SetActive(false);
    }
}
