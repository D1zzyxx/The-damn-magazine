using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Item slotItem; //предмет в слоте
    public GameObject ItemObj; //предмет игровой

    Image icon;
    Button button;
    private void Start()
    {
        //получаем компоненты
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(slotsClicked);
    }

    public void PutInSlot(Item item, GameObject obj)
    {
        //калдем предметы в слот, заполняя компоненты слота компонентами предмета
<<<<<<< HEAD
        icon.color = Color.white;
        icon.sprite = item.icon;
        slotItem = item;
        icon.enabled = true;
        ItemObj = obj;        
=======
        icon.sprite = item.icon;
        slotItem = item;
        icon.enabled = true;
        ItemObj = obj;
>>>>>>> origin/branchIlgizar
    }

    void slotsClicked()
    {
        //если слот не пустой, то открывается окно инфы слота
        if (slotItem != null)
            ItemInfo.Instance.ShowInfo(slotItem, ItemObj, this); //пеедаем инфу о предмете в лоте в окно
    }

    public void ClearSlot()
    {
        //очищаем слот
        slotItem = null;
        ItemObj = null;
        icon.sprite = null;
<<<<<<< HEAD
        icon.enabled = false;
=======
>>>>>>> origin/branchIlgizar
    }
}
