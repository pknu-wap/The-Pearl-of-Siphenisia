// MonoBehavior�� ��ӹ��� ������, ���ӿ�����Ʈ�� ���� �ʿ䰡 ����.
// https://glikmakesworld.tistory.com/14

using UnityEngine;

[System.Serializable]
public class SaveData
{
    [Header("��")]
    // ���� ���� �ְ� ��������
    public int mapNumber;
    // �ְ� ���������� ����(������ �ĺ� �� �ϳ�)
    public int mapIndex;
    
    [Header("�κ��丮")]
    public string[] inventoryItems = new string[90];

    [Header("����")]
    public int targetFPS;
    public int vSyncCount;
    public float soundVolume;

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventoryItems = new string[90];

        targetFPS = 60;
        vSyncCount = 0;
        soundVolume = 1;
    }
}