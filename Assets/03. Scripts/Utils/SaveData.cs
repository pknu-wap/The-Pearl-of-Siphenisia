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
    public string[] inventoryItems = new string[90];

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventoryItems = new string[90];
    }
}
