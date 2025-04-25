using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; //создаем статический объект класса дл€ того, чтобы обращатьс€ к нему из других скриптов
   
    public Transform SlorsParent; //получаем его значени€ компонента трансформ
    public InventorySlots[] inventorySlots = new InventorySlots[5]; //массивом элементов класса InventorySlots обозначаем количество слотов

    public List<InventorySlots> slots = new List<InventorySlots>(5);

    private void Start()
    {
        instance = this;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = SlorsParent.GetChild(i).GetComponent<InventorySlots>(); //через перебор и метод GetChild получаем все слоты
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
}
