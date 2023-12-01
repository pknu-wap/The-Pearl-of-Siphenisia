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

        playerCollision.onPlayerDamaged.AddListener(DestroyItem);

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

        playerCollision.onPlayerDamaged.RemoveListener(DestroyItem);

        return true;
    }

    public void DestroyItem()
    {
        DeactivateItem();
        ClearSlot();
        Destroy(gameObject);
    }

    void ClearSlot()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        slot.ClearSlot();
    }
}
