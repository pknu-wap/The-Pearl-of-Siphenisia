using UnityEngine;

public class BombItem : Item
{
    bool isHanded = false;

    public override void ActivateItem()
    {
        isHanded = true;
    }

    public override void DeactivateItem()
    {
        isHanded = false;
    }

    private void Update()
    {
        if(isHanded == false)
        {
            return;
        }

        if(Input.GetKey(KeyCode.Q))
        {
            AimBomb();

            if(Input.GetMouseButtonDown(1))
            {
                Unaim();
            }

            else if(Input.GetMouseButtonDown(0))
            {
                ThrowBomb();
                Destroy(this);
            }
        }
    }

    private void AimBomb()
    {
        Debug.Log("Aimed");
    }

    private void Unaim()
    {
        Debug.Log("Unaimed");
    }

    private void ThrowBomb()
    {
        Debug.Log("Throwed");
    }
}
