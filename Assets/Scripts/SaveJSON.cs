using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveJSON : MonoBehaviour
{
    public Inventory _inventory;
    public string _fileName;
    public InventorySlots[] slots;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SaveData();
            Debug.Log("На ЛКМ Сохранение сработало");
        }
    }

    public void SaveData()
    {
        // Сохраняем в потоке файл
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileName);
        /* К for!
         * Я указал длинну инвенторя ЯВНО!
         * Сейчас это костыль и работает только если у нас инвентарь в 5 слотов
         */
        for (int i = 0; i < 5; i++)
        {
            string json = JsonUtility.ToJson(i);
            sw.WriteLine(json);
        }
        sw.Close();
        Debug.Log("Данные сохранились");
    }
    public void LoadData()
    {
        if (File.Exists((Application.persistentDataPath + "/" + _fileName)))
        {
            string[] reded = File.ReadAllLines((Application.persistentDataPath + "/" + _fileName));

            for (int i = 0; i < reded.Length; i++)
            {
                InventorySlots newItem = JsonUtility.FromJson<InventorySlots>(reded[i]);

                Inventory.instance.slots.Add(newItem);
            }
            Debug.Log("Загрузились ли данные???");
        }
    }
}
