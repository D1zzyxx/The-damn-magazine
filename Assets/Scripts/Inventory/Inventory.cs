using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; //создаем статический объект класса для того, чтобы обращаться к нему из других скриптов
    public GameObject Inv;
   
    public Transform SlorsParent; //получаем его значения компонента трансформ
    public InventorySlots[] inventorySlots = new InventorySlots[5]; //массивом элементов класса InventorySlots обозначаем количество слотов

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

    public void PutEmptySlot(Item item, GameObject obj) //метод, который делает проверку свободных слотов и кладет предмет в первый свободный слот
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
                    // Создаем новый объект предмета
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
