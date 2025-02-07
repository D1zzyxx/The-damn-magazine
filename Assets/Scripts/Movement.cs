using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int check = 0;
    public TextMeshProUGUI TMPUGUI;

    //Данный код создан ИСКЛЮЧИТЕЛЬНО В КАЧЕСТВЕ ПРОВЕРКИ!
    //Он не несет в себе никакой смысловой нагрузки
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            check++;
            Debug.Log("Очень сложный скрипт на проверку изменений в репозитории");
            Debug.Log($"Количество кликов: {check}");

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (TMPUGUI != null)
        {
            TMPUGUI.text = "Кликов: " + check.ToString();
        }
    }
}
