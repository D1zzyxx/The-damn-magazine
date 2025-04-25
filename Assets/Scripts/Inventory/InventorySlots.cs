using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Item slotItem; //������� � �����
    public GameObject ItemObj; //������� �������

    Image icon;
    Button button;
    private void Start()
    {
        //�������� ����������
        icon = gameObject.transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(slotsClicked);
    }

    public void PutInSlot(Item item, GameObject obj)
    {
        //������ �������� � ����, �������� ���������� ����� ������������ ��������
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
        //���� ���� �� ������, �� ����������� ���� ���� �����
        if (slotItem != null)
            ItemInfo.Instance.ShowInfo(slotItem, ItemObj, this); //������� ���� � �������� � ���� � ����
    }

    public void ClearSlot()
    {
        //������� ����
        slotItem = null;
        ItemObj = null;
        icon.sprite = null;
<<<<<<< HEAD
        icon.enabled = false;
=======
>>>>>>> origin/branchIlgizar
    }
}
