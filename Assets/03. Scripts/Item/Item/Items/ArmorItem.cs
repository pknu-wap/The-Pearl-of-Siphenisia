using UnityEngine;

public class ArmorItem : Item
{
    public override void ActivateItem()
    {
        // °©¿ÊÀ» ÇÃ·¹ÀÌ¾î¿¡°Ô Ãß°¡ÇÑ´Ù.
        Debug.Log("°©¿Ê ÀåÂø");
    }


    public override void DeactivateItem()
    {
        // °©¿ÊÀ» »èÁ¦ÇÑ´Ù.
        Debug.Log("°©¿Ê ÇØÁ¦");
    }
}
