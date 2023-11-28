// https://glikmakesworld.tistory.com/14
// https://gist.github.com/TarasOsiris/9020497

using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Events;

public class SaveManager : Singleton<SaveManager>
{
    public static SaveData saveData;

    private static string privateKey;
    public UnityEvent SaveAll;

    protected override void Awake()
    {
        base.Awake();

        privateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

        saveData = Load();

        if (saveData == null)
        {
            Debug.Log("�� saveData ����");
            saveData = new SaveData();
        }
    }

    /// <summary>
    /// saveData�� ���Ϸ� �����Ѵ�.
    /// </summary>
    public void Save()
    {
        SaveAll.Invoke();

        string jsonString = DataToJson(saveData);
        string encryptString = Encrypt(jsonString);
        SaveFile(encryptString);
    }

    /// <summary>
    /// saveData�� ������ �ҷ��´�.
    /// </summary>
    /// <returns></returns>
    public static SaveData Load()
    {
        if(File.Exists(GetPath()) == false)
        {
            return null;
        }

        string encryptData = LoadFile(GetPath());
        string decryptData = Decrypt(encryptData);

        Debug.Log(decryptData);

        saveData = JsonToData(decryptData);
        return saveData;
    }

    #region ������ ����
    #region �κ��丮
    public void SaveInventory(Slot[][] slots)
    {
        // ����� �����ʹ� 2���� �迭�� ���η� ��ģ 1���� �迭
        for (int i = 0; i < slots.Length; i++)
        {
            for (int j = 0; j < slots[i].Length; j++)
            {
                if (slots[i][j].slotItem == null)
                {
                    saveData.inventoryItems[(i * 30 + j)] = null;
                    continue;
                }

                saveData.inventoryItems[(i * 30 + j)] = slots[i][j].slotItem.itemData.itemCode;
            }
        }
    }

    public string[] LoadInventory()
    {
        return saveData.inventoryItems;
    }
    #endregion �κ��丮

    #region ����
    public void SaveTargetFPS(int targetFPS)
    {
        saveData.targetFPS = targetFPS;
    }

    public int LoadTargetFPS()
    {
        return saveData.targetFPS;
    }

    public void SaveVSyncCount(int vSyncCount)
    {
        saveData.vSyncCount = vSyncCount;
    }

    public int LoadVSyncCount()
    {
        return saveData.vSyncCount;
    }

    public void SaveSoundVolume(float soundVolume)
    {
        saveData.soundVolume = soundVolume;
    }

    public float LoadSoundVolume()
    {
        return saveData.soundVolume;
    }
    #endregion ����

    public void SaveMapNumber(int currentMap)
    {
        saveData.mapNumber = currentMap;
    }

    public int LoadMapNumber()
    {
        return saveData.mapNumber;
    }

    public void SaveMapIndex(int currentMap)
    {
        saveData.mapIndex = currentMap;
    }

    public int LoadMapIndex()
    {
        return saveData.mapIndex;
    }
    #endregion ������ ����

    #region ���� ����
    static string DataToJson(SaveData saveData)
    {
        string jsonData = JsonUtility.ToJson(saveData);
        return jsonData;
    }

    static SaveData JsonToData(string jsonData)
    {
        SaveData saveData = JsonUtility.FromJson<SaveData>(jsonData);
        return saveData;
    }

    static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(GetPath(), FileMode.Create, FileAccess.Write))
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            fs.Write(bytes, 0, bytes.Length);
        }
    }

    static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[(int)fs.Length];

            fs.Read(bytes, 0, (int)fs.Length);

            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }

    private static string Encrypt(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(results, 0, results.Length);
    }

    private static string Decrypt(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(resultArray);
    }

    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;
        result.Mode = CipherMode.ECB;
        result.Padding = PaddingMode.PKCS7;
        return result;
    }

    static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "save.sv");
    }
    #endregion ���� ����
}