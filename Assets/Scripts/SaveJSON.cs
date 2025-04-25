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
            Debug.Log("�� ��� ���������� ���������");
        }
    }

    public void SaveData()
    {
        // ��������� � ������ ����
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + _fileName);
        /* � for!
         * � ������ ������ ��������� ����!
         * ������ ��� ������� � �������� ������ ���� � ��� ��������� � 5 ������
         */
        for (int i = 0; i < 5; i++)
        {
            string json = JsonUtility.ToJson(i);
            sw.WriteLine(json);
        }
        sw.Close();
        Debug.Log("������ �����������");
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
            Debug.Log("����������� �� ������???");
        }
    }
}
