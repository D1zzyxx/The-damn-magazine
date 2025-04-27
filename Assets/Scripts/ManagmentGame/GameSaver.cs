using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public static GameSaver Instance;

    public PlayerData playerData;
    public SaveData saveData = new SaveData();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SaveInventory()
    {
        saveData.inventory = Inventory.instance.GetSaveData();
        Debug.Log("������� ���������");
    }

    public void LoadInventory()
    {
        Inventory.instance.LoadSaveData(saveData.inventory);
        Debug.Log("�������� ���������");
    }
}

[System.Serializable]
    public class PlayerData
    {
    public Vector3 position;
    public int health;
    //public InventorySlots[] inventory;
    // �������� ������ ������ ���������    
    }
[System.Serializable]
public class SaveData
{
    public string[] inventory = new string[5];
}


