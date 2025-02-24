using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public static ItemUse Instance;
    void Start()
    {
        Instance = this;
    }

    public void Use(Item item)
    {
        if (item.isHealing)
        {
            //Playre.Instanse.health =+ healingPower;
            Debug.Log("+" + item.healingPower);
        }
    }
  
}
