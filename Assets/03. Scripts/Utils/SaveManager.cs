// https://glikmakesworld.tistory.com/14
// https://gist.github.com/TarasOsiris/9020497

using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveManager : Singleton<SaveManager>
{
    public static SaveData saveData;

    private static string privateKey;

    public Inventory inventory;

    protected override void Awake()
    {
        base.Awake();

        privateKey = SystemInfo.deviceUniqueIdentifier.Replace("-", string.Empty);

        AssignObjects();

        saveData = Load();

        if (saveData == null)
        {
            saveData = new SaveData();
            Save();
        }

        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();
    }

    private void AssignObjects()
    {
        try
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        catch { }
    }

    /// <summary>
    /// saveData를 파일로 저장한다.
    /// </summary>
    public void Save()
    {
        SaveInventory(inventory.slots);

        string jsonString = DataToJson(saveData);
        string encryptString = Encrypt(jsonString);
        SaveFile(encryptString);
    }

    /// <summary>
    /// saveData에 파일을 불러온다.
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

    #region 데이터 저장
    public void SaveInventory(Slot[][] slots)
    {
        saveData = new SaveData();
        
        // 저장된 데이터는 2차원 배열을 가로로 펼친 1차원 배열
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
    #endregion 데이터 저장

    #region 내부 구현
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
    #endregion 내부 구현
}
