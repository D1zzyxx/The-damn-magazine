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

    private void OnTriggerEnter2D(Collider2D other) //метод который при столкновении игрока и предмета, вызывает метод PutEmptySlot и выключает предмет на сцене
    {
        if (other.CompareTag("Player"))
        {           
            Inventory.instance.PutEmptySlot(item, itemObj);
            gameObject.SetActive(false);
        }
    }
}
