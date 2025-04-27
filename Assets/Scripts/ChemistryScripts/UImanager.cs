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
    [SerializeField] private TMP_Text InputText, QuestionText; // InputText - текст введённого ответа (в коде не используется явно)
    // QuestionText - текст текущего вопроса, отображается пользователю
    [SerializeField] private TMP_InputField inputField; // Поле ввода, куда пользователь вводит ответ
    [SerializeField] private string MyText; 
    public GameObject panels; // Панель с вопросами
    public GameObject panelsDialogue; // Панель диалога, которая показывается после теста
    public Button continues; //Кнопка продолжения
    string inputText;
    int index = 0; // Индекс текущего вопроса (начинается с 0)
    int comparies = 0;// Счётчик правильных ответов
    public static bool OpenQuests = false; // Флаг, указывающий, что тест завершён и можно открыть диалог

    List<string> managers = new List<string>() 
    {"Формула воды", "Формула спирта Металон", "Какой элемент называют \"желчью бога Вулкана\"?" };
    // Список вопросов
    private void Awake()
    {
        QuestionText.text = managers[index]; //Устанавливает текст в поле для первого вопроса
    }
    private void Update()
    {
        continues.onClick.AddListener(SaveInputText);
        //При нажатии кнопки continues, буудет вызываться метод SaveInputText
    }
    
    public void SaveInputText() //Функция сравнения вводимого ответа
    {
        if(index >= managers.Count) // Проверяет, достигнут ли конец списка вопросов
        {
            PlayerPrefs.SetInt("Comparies", comparies);//Сохраняет количество правильных ответов
            PlayerPrefs.Save();//Сохраняет изменения
            panels.SetActive(false);//Отключает панель с тестом
            OpenQuests = true;//Устанавливает значение для OpenQuests в true, сигнализируя другим скриптам, что тест завершён.
            panelsDialogue.SetActive(true);//Включает панель диалога
        }
        if (index == 0) Question1();
        else if (index == 1) Question2();
        else if (index == 2) Question3();
        //Вызывает соответствующий метод QuestionX() в зависимости от текущего индекса
    }
    private void Question1() //Сравнение 1 ответа
    {
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
            if (inputField.text == "СЕРА")
            {
                comparies += 1;
            }
            inputField.text = "";
            ++index;
            Debug.Log(comparies);
        }        
    }
}
