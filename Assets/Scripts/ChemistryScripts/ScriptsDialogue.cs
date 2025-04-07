using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;

public class ScriptsDialogue : MonoBehaviour
{
    public GameObject dialogManager;
    public GameObject testChemistryPanels;
    public TMP_Text textTeachers;
    public TMP_Text textZinas;
    public TMP_Text NameTeachers;
    public TMP_Text NameZinas;
    int index = 0;
    int compare;
    bool OpenTest = false;

    List<string> NameTeacher = new List<string>()
    {
        "Василиса Михайлова:",
        "",
        "Василиса Михайлова: ","Василиса Михайлова: ",
        "",
        "Василиса Михайлова: "
    };
    List<string> NameZina = new List<string>() 
    {
        "",
        "Зина", "",
        "",
        "Зина",""
    };
    List<string> textTeacher = new List<string>()
    {
        "Что ты здесь забыла? Опять что-то нужно? ",
        "", "Какой ещё журнал?! Никакого журнала у меня нет! Лучше думай о том, как пересдать химию!",
        "У тебя уже столько долгов накопилось, что я удивляюсь, как ты ещё на занятия ходишь!",
        "","А должна была сдать с первого раза! Химия – это не шутки. " +
        "Если есть время на споры, то значит будешь пересдавать сейчас-же."
    };
    List<string> textZina = new List<string>()
    {
        "",
        "Здравствуйте. У вас, случайно, нет журнала нашей группы?",
        "","",
        "Ну, я же хожу к вам. Недавно была на 76й пересдаче!",
        ""
    };
    List<string> text_Name = new List<string>()
    {
        "Василиса Михайлова:","Василиса Михайлова:"
    };
    List<string> text_0 = new List<string>()
    {
       "Эх, Зина.","Я так и знала, что студент, как ты, не готовилась. Проваливай с моих глаз долой. Жду тебя в сентябре на 78ю пересдачу!"
    };
    List<string> text_1_2 = new List<string>()
    {
        "Эх, Зина.","Ты ведь не готовилась, да? Но для тебя, бездыря, такой результат считается еще неплохим. Не забывай, жду тебя в сентябре на 78ю пересдачу! "
    };
    List<string> text_3 = new List<string>()
    {
        "Ого! Удивительный результат для такого студента, как ты. Вот видишь. Немного постаралась и сразу сдала! ",
        "Тебе осталось еще 30 контрольных и получишь оценку! Жду тебя в сентябре на 78ю пересдачу!"
    };
    private void Awake()
    {
        textTeachers.text = textTeacher[index];
        textZinas.text = textZina[index];
        NameTeachers.text = NameTeacher[index];
        NameZinas.text = NameZina[index];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (OpenTest == false)
            {
                NextText();
            }
            else
            {
                compare = PlayerPrefs.GetInt("Comparies");                
                if (compare == 0)
                {                    
                    WinText_1();
                }
                else if (compare == 3)
                {                   
                    WinText_3();
                }
                else
                {  
                    WinText_2();
                }
            }
        }        
    }
    void WinText_1()
    {
       
        if (index < text_Name.Count)
        {
            NameTeachers.text = text_Name[index];
            textTeachers.text = text_0[index];
        }
        else
        {
            Close();
        }
        index++;
    }
    void WinText_2()
    { 
        if (index < text_Name.Count)
        {
            NameTeachers.text = text_Name[index];
            textTeachers.text = text_1_2[index];
        }
        else
        {
            Close();
        }
        index++;
    }
    void WinText_3()
    {
        if (index < text_Name.Count)
        {
            NameTeachers.text = text_Name[index];
            textTeachers.text = text_3[index];
        }
        else
        {
            Close();
        }
        index++;
    }

    void NextText()
    {
        index++;
        if (index < textTeacher.Count)
        {
            textTeachers.text = textTeacher[index];
            textZinas.text = textZina[index];
            NameTeachers.text = NameTeacher[index];
            NameZinas.text = NameZina[index];
        }
        else
        {
            Close();
            testChemistryPanels.SetActive(true);
            OpenTest = true;
            index = 0;
        }
    }
    void Close()
    {
        dialogManager.SetActive(false);
    }
}
