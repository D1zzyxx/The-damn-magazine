using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; //������� ����������� ������ ������ ��� ����, ����� ���������� � ���� �� ������ ��������
    public GameObject Inv;
   
    public Transform SlorsParent; //�������� ��� �������� ���������� ���������
    public InventorySlots[] inventorySlots = new InventorySlots[5]; //�������� ��������� ������ InventorySlots ���������� ���������� ������

    public List<InventorySlots> slots = new List<InventorySlots>(5);

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = SlorsParent.GetChild(i).GetComponent<InventorySlots>(); //����� ������� � ����� GetChild �������� ��� �����
        }
    }
    private void Start()
    {        
        InitializeSlots();
        GameSaver.Instance.LoadInventory();
    }
    void InitializeSlots()
    {
        inventorySlots = new InventorySlots[SlorsParent.childCount];
        for (int i = 0; i < SlorsParent.childCount; i++)
        {
            inventorySlots[i] = SlorsParent.GetChild(i).GetComponent<InventorySlots>();
        }
    }

    public void PutEmptySlot(Item item, GameObject obj) //�����, ������� ������ �������� ��������� ������ � ������ ������� � ������ ��������� ����
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].slotItem == null)
            {
                inventorySlots[i].PutInSlot(item, obj);
                return;
            }
        }
    }
    public string[] GetSaveData()
    {
        string[] saveSlots = new string[inventorySlots.Length];
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            saveSlots[i] = inventorySlots[i].slotItem?.Name;
        }
        return saveSlots;
    }

    public void LoadSaveData(string[] savedSlots)
    {
        for (int i = 0; i < savedSlots.Length; i++)
        {
            if (!string.IsNullOrEmpty(savedSlots[i]))
            {
                Item item = Resources.Load<Item>("Item/" + savedSlots[i]);
                if (item != null)
                {
                    // ������� ����� ������ ��������
                    GameObject newItem = new GameObject();
                    newItem.AddComponent<SpriteRenderer>().sprite = item.icon;
                    newItem.AddComponent<PickUpItem>().item = item;
                    newItem.SetActive(false);

                    inventorySlots[i].PutInSlot(item, newItem);
                }
            }
        }
    }
}
