using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//����� � ���� ��� �����...
public class ItemInfo : MonoBehaviour
{
    //��������� ���������� ���� ���������� �� �������
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
        //�������� ���������� ��� ����������� �������������, ��, ���������...
        Instance = this;
        BackGround = GetComponent<Image>();
        Title = transform.GetChild(0).GetComponent<Text>();
        Description = transform.GetChild(1).GetComponent<Text>();
        Icon = transform.GetChild(2).GetComponent<Image>();
        DescriptionLore = transform.GetChild(3).GetComponent<Text>();
    }

    public void ChangeInfo(Item item) //��������� ��� ���������� �������� �� ���������� ����
    {
        Title.text = item.Name;
        Description.text = item.Discriptin;
        Icon.sprite = item.icon;
        DescriptionLore.text = item.DiscriptinLore;
    }
    public void Drop() //����������� �������
    {       
        Vector2 DropPos = new(Player.Instance.transform.position.x + 3f, Player.Instance.transform.position.y); //�������� ������� ������ � ���� ������� ����� �� �� ����� ������ ����
        ItemObj.SetActive(true); //������� ������� �� �����
        ItemObj.transform.position = DropPos; //��������� ���������� �������� ������
        CurrentSlot.ClearSlot(); //������ ����
        OffInfo(); 
    }

    public void UseItem()
    {
        ItemUse.Instance.Use(InfoItem);
        CurrentSlot.ClearSlot();
        OffInfo();
    }

    public void ShowInfo(Item item, GameObject itemObj, InventorySlots currentSlot)
    {
        ChangeInfo(item);
        InfoItem = item;
        ItemObj = itemObj;
        CurrentSlot = currentSlot;

        gameObject.transform.localScale = Vector3.one; 

    }
    public void OffInfo() //��������� ����
    {
        gameObject.transform.localScale = Vector3.zero;
    }
}
