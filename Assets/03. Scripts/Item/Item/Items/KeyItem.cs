using UnityEngine;

public class KeyItem : HandItem
{
    public Cubic cubic;

    PlayerCollision player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindWithTag("Player").GetComponent<PlayerCollision>();
    }

    public override bool ActivateItem()
    {
        if(cubic == player.currentKeySword.cubic)
        {
            player.DestroyKeySword();
            DestroyItem();
            return true;
        }

        else
        {
            GameUIManager.Instance.AlertMessage("이 보석이 아닌 듯하다.");
        }
        return false;
    }

    public override bool DeactivateItem()
    {
        return true;
    }
}
