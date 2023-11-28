// MonoBehavior를 상속받지 않으면, 게임오브젝트에 붙일 필요가 없다.
// https://glikmakesworld.tistory.com/14

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // 진행 중인 최고 스테이지
    public int mapNumber;
    // 최고 스테이지의 종류(랜덤한 후보 중 하나)
    public int mapIndex;
    public List<Item>[] inventory;

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventory = new List<Item>[3];

        for(int i = 0; i < 3; i++)
        {
            inventory[i] = new List<Item>(new Item[30]);
        }
    }
}
