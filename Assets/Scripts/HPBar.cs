using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private float HP;
    public Image Bar;
    //HP --1;
    //private Bar.fillAmount = HP/5;
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Hearts")) HP = 5f; //Проверяет наличие ключа "Hearts". При его отсутствии, HP будет равен 5
        else PlayerPrefs.GetFloat("Hearts", HP);  // Иначе HP равен данным, которые были сохранены ранее
    }
    private void FixedUpdate()
    {
        
    }
    void OnCollisionEnter2D(Collision2D colDoor) //При взаимодействии с тегом Door, происходит запись количества жизней
    {
        if (colDoor.gameObject.tag == "Door")
        {
            CloseScene();
        }
    }
    void CloseScene()
    {
        PlayerPrefs.SetFloat("Hearts", HP); //Сохранение в ключе "Hearts" данных о количестве жизней
    }
}
 