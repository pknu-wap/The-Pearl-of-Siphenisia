// MonoBehavior를 상속받지 않으면, 게임오브젝트에 붙일 필요가 없다.
// https://glikmakesworld.tistory.com/14

using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("맵")]
    // 진행 중인 최고 스테이지
    public int mapNumber;
    // 최고 스테이지의 종류(랜덤한 후보 중 하나)
    public int mapIndex;
    
    [Header("인벤토리")]
    public string[] inventoryItems = new string[90];

    [Header("설정")]
    public int targetFPS;
    public int vSyncCount;
    public float masterValue;
    public float bgmValue;
    public float sfxValue;

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventoryItems = new string[90];

        targetFPS = 60;
        vSyncCount = 0;
        masterValue = 0;
        bgmValue = 0;
        sfxValue = 0;
}
}
