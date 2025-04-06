using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;

public class UImanager : MonoBehaviour
{
    [SerializeField] private TMP_Text InputText, QuestionText; //Переменные для: текста вопроса, вводимого текста
    [SerializeField] private TMP_InputField inputField; //Переменная для сравнения вводимого текста и прав ответа
    [SerializeField] private string MyText; //Что-то
    public GameObject panels;
    public Button continues; //Ко
    string inputText;
    int index = 0;
    int comparies = 0;
    List<string> managers = new List<string>() {"Формула воды", "Формула спирта Металон", "Какой элемент называют \"желчью бога Вулкана\"?" };
    private void Awake()
    {
        QuestionText.text = managers[index];
        
    }
    private void Update()
    {
        continues.onClick.AddListener(SaveInputText);
    }
    
    public void SaveInputText() //Функция сравнения вводимого ответа
    {
        if(index >= managers.Count)
        {
           
        }
        if (index == 0) Question1();
        else if (index == 1) Question2();
        else if (index == 2) Question3();
        
    }   
    private void Question1() //Сравнение 1 ответа
    {
        //inputText = inputField.text;
        if (inputField.text != "")
        {           
            if (inputField.text == "H2O")
            {
                comparies += 1;
            }
            inputField.text = "";
            ++index;
            Debug.Log(comparies);
            QuestionText.text = managers[index];
        }
    }
    private void Question2()//Сравнение 2 ответа
    {
        
        if (inputField.text != "")
        {   
            if (inputField.text == "CH3OH")
            {
                comparies += 1;
            }
            inputField.text = "";
            ++index;
            Debug.Log(comparies);
            QuestionText.text = managers[index];
        }
       
    }
    private void Question3()//Сравнение 3 ответа
    {
        if (inputField.text != "")
        {
            if (inputField.text == "CEPA")
            {
                comparies += 1;
            }
            inputField.text = "";
            ++index;
            Debug.Log(comparies);
        }
        
    }
}
