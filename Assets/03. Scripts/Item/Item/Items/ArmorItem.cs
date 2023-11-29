using UnityEngine;

public class ArmorItem : Item
{
    private PlayerCollision playerCollision;
    private GameObject barrier;

    private void Awake()
    {
        playerCollision = GameObject.FindWithTag("Player").GetComponent<PlayerCollision>();
        barrier = playerCollision.transform.GetChild(2).gameObject;
    }

    public override bool ActivateItem()
    {
        if(playerCollision.currentArmor != null)
        {
            return false;
        }

        barrier.SetActive(true);
        playerCollision.isArmored = true;
        playerCollision.currentArmor = this;

        return true;
    }


    public override bool DeactivateItem()
    {
        if (playerCollision.currentArmor == null)
        {
            return false;
        }

        barrier.SetActive(false);
        playerCollision.isArmored = false;
        playerCollision.currentArmor =  null;

        return true;
    }

    public void DestroyItem()
    {
        DeactivateItem();
        Destroy(gameObject);
    }
}
