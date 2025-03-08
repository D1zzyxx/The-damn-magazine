using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;

public class UImanager : MonoBehaviour
{
    [SerializeField] private TMP_Text InputText, QuestionText; //Переменные для: текста вопроса, вводимого текста
    [SerializeField] private TMP_InputField inputField; //Переменная для сравнения вводимого текста и прав ответа
    [SerializeField] private string MyText; //Что-то

    public Button continues;
    int count = 0;
    List<string> managers = new List<string>() { "Формула воды", "Формула спирта Металон", "Какой элемент называют \"желчью бога Вулкана\"?" };
    //List<string> answer = new List<string>() { "H2O", "CH3OH", "СЕРА" };
    private void Update()
    {
        
    }
    public void SaveInputText() //Функция сравнения вводимого ответа
    {
        MyText = InputText.text;
        ShowText();
    }
    void ShowText() //функция происходящая при нажатии на кнопку продолжения ответа, но не доделанная
    {
    continues.onClick.AddListener
        (
            delegate ()
            {
                for (int i = 0; i < 3; i++) 
                {
        
                    if (i == 0) Question1();
                    if (i == 1) Question2();
                    if (i == 2) Question3();
                }
            }
        );
    }
    private void Question1() //Сравнение 1 ответа
    {
        QuestionText.text = managers[0];
        
        if (inputField.text == "H2O") 
        { 
            count++;
        }
    }
    private void Question2()//Сравнение 2 ответа
    {
        QuestionText.text = managers[1];
        if (inputField.text == "CH3OH")
        {
            count++;
        }
    }
    private void Question3()//Сравнение 3 ответа
    {
        QuestionText.text = managers[2];
        if (inputField.text == "СЕРА")
        {
            count++;
        }
    }
}
