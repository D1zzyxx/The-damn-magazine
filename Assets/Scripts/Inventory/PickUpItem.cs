using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;
    private GameObject itemObj;

    private void Start()
    {
        itemObj = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D other) //����� ������� ��� ������������ ������ � ��������, �������� ����� PutEmptySlot � ��������� ������� �� �����
    {
        if (other.CompareTag("Player"))
        {           
            Inventory.instance.PutEmptySlot(item, itemObj);
            gameObject.SetActive(false);
        }
    }
}
