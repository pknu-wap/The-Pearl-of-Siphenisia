using UnityEngine;

public class ArmorItem : Item
{
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public override void ActivateItem()
    {
        // TODO: 보호막 이펙트를 플레이어에 추가 -> 활성화/비활성화로 조작
        // player.armor++;, 보호 수준 변경하기
    }


    public override void DeactivateItem()
    {
        // TODO: 보호막 이펙트를 플레이어에 추가 -> 활성화/비활성화로 조작
        // player.armor--;, 보호 수준 변경하기
        // 개인적으론 armor는 0 혹은 1로 하고, 대입으로 세팅하는 게 더 좋을 듯
    }
}
