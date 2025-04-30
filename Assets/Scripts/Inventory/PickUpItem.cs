using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item; // Ссылка на предмет
    public GameObject hintText; 
    private bool isPlayerInTrigger = false; // Флаг для проверки, находится ли игрок в зоне триггера

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

            // Показываем текст подсказки
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

            // Скрываем текст подсказки
            if (hintText != null)
            {
                hintText.SetActive(false);
            }
        }
    }

    private void Update()
    {
        // Проверяем, находится ли игрок в зоне триггера и нажата ли клавиша E
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            UseItem(item);
        }
    }

    private void UseItem(Item item)
    {
        if (item != null)
        {
            Debug.Log($"Предмет {item.Name} используется!");

            switch(item.itemType) //проверяем к какому типу принадлежит предмет и делаем те дела, которые к ним относятся
            {
                case ItemType.Healing:
                    HPBar.instanse.HP += item.powerHeal;
                    Debug.Log("Захилились!");
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
                Destroy(gameObject); // Удаляем текущий объект
            }

            // Скрываем текст подсказки
            if (hintText != null)
            {
                hintText.SetActive(false);
            }
        }
    }
}
