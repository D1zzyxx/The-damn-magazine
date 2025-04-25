using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; //������� ����������� ������ ������ ��� ����, ����� ���������� � ���� �� ������ ��������
   
    public Transform SlorsParent; //�������� ��� �������� ���������� ���������
    public InventorySlots[] inventorySlots = new InventorySlots[5]; //�������� ��������� ������ InventorySlots ���������� ���������� ������

    public List<InventorySlots> slots = new List<InventorySlots>(5);

    private void Start()
    {
        instance = this;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i] = SlorsParent.GetChild(i).GetComponent<InventorySlots>(); //����� ������� � ����� GetChild �������� ��� �����
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
}
