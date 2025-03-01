using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private float HP;
    public Image Bar;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Hearts")) HP = 5f; //Проверяет наличие ключа "Hearts". При его отсутствии, HP будет равен 5
        else PlayerPrefs.GetFloat("Hearts", HP);  // Иначе HP равен данным, которые были сохранены ранее
    }
    private void FixedUpdate()
    {
        if (HP ==0) //При нулевом хп переносит на новую сцену
        {
            YouLose();
        }
    }
    void OnCollisionEnter2D(Collision2D collision) //При взаимодействии с тегом Door, происходит запись количества жизней
    {
        if (collision.gameObject.tag == "Door") //При взаимодействии с тегом Door, происходит запись количества жизней
        {
            CloseScene();
        }
        if (collision.gameObject.tag == "Block") //При взаимодействии с тегом Block, уменьшается количество жизней
        {
            HP -= 1;
            Bar.fillAmount = HP / 5;
        }
    }
    void CloseScene()
    {
        PlayerPrefs.SetFloat("Hearts", HP); //Сохранение в ключе "Hearts" данных о количестве жизней
    }
    public void YouLose() //Переход на другую сцену
    {
        SceneManager.LoadScene(2);
    }
}
 