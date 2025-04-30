using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item; // ������ �� �������
    public GameObject hintText; 
    private bool isPlayerInTrigger = false; // ���� ��� ��������, ��������� �� ����� � ���� ��������

    private void Start()
    {      
        if (hintText != null)
        {
            hintText.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;

            // ���������� ����� ���������
            if (hintText != null)
            {
                hintText.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;

            // �������� ����� ���������
            if (hintText != null)
            {
                hintText.SetActive(false);
            }
        }
    }

    private void Update()
    {
        // ���������, ��������� �� ����� � ���� �������� � ������ �� ������� E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            UseItem(item);
        }
    }

    private void UseItem(Item item)
    {
        if (item != null)
        {
            Debug.Log($"������� {item.Name} ������������!");

            switch(item.itemType) //��������� � ������ ���� ����������� ������� � ������ �� ����, ������� � ��� ���������
            {
                case ItemType.Healing:
                    HPBar.instanse.HP += item.powerHeal;
                    Debug.Log("����������!");
                    break;
                case ItemType.Magazine:
                    Player.Instance.pickUpMagazine = true;
                    break;
                case ItemType.Book:
                    Player.Instance.pickUpBook = true;
                    break;
            }
            if (gameObject != null)
            {
                Destroy(gameObject); // ������� ������� ������
            }

            // �������� ����� ���������
            if (hintText != null)
            {
                hintText.SetActive(false);
            }
        }
    }
}
