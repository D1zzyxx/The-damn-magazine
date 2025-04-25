using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//зачем € пишу так много...
public class ItemInfo : MonoBehaviour
{
    //объ€вл€ем переменные окна информации об объекте
    public static ItemInfo Instance;
    private Image BackGround;
    private Text Title;
    private Text Description;
    private Image Icon;
    private Text DescriptionLore;

    private Item InfoItem;
    private GameObject ItemObj;
    private InventorySlots CurrentSlot;


    private void Start()
    {
        //получаем компоненты дл€ дальнейшего использовани€, да, костыльно...
        Instance = this;
        BackGround = GetComponent<Image>();
        Title = transform.GetChild(0).GetComponent<Text>();
        Description = transform.GetChild(1).GetComponent<Text>();
        Icon = transform.GetChild(2).GetComponent<Image>();
        DescriptionLore = transform.GetChild(3).GetComponent<Text>();
    }

    public void ChangeInfo(Item item) //назначает все компоненты предмета на компоненты окна
    {
        Title.text = item.Name;
        Description.text = item.Discriptin;
        Icon.sprite = item.icon;
        DescriptionLore.text = item.DiscriptinLore;
    }
    public void Drop() //выбрасываем предмет
    {       
        Vector2 DropPos = new(Player.Instance.transform.position.x + 3f, Player.Instance.transform.position.y); //получаем позицию игрока и чуть плюсуем чтобы не на месте игрока было
        ItemObj.SetActive(true); //врубаем предмет на сцене
        ItemObj.transform.position = DropPos; //назначаем выкинутому предмету дропоз
        CurrentSlot.ClearSlot(); //чистим слот
        OffInfo(); 
    }

    public void UseItem()
    {
        ItemUse.Instance.Use(InfoItem);
<<<<<<< HEAD
        CurrentSlot.ClearSlot();
        OffInfo();
=======
>>>>>>> origin/branchIlgizar
    }

    public void ShowInfo(Item item, GameObject itemObj, InventorySlots currentSlot)
    {
        ChangeInfo(item);
        InfoItem = item;
        ItemObj = itemObj;
        CurrentSlot = currentSlot;

        gameObject.transform.localScale = Vector3.one; 

    }
    public void OffInfo() //закрываем окно
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
