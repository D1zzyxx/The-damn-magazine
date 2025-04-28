using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChemistry : MonoBehaviour
{
    public GameObject panel;
    int count;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Used")) count = 0; //Проверяет наличие ключа "Used". При его отсутствии, count будет равен 0
        else PlayerPrefs.GetInt("Used", count);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "TeacherChemistry")&&(count == 0))
        {
            panel.SetActive(true);
            count = 1;
            PlayerPrefs.SetInt("Used", count);
            PlayerPrefs.Save();
        }
    }
}
