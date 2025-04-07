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
        "�������� ���������:",
        "",
        "�������� ���������: ","�������� ���������: ",
        "",
        "�������� ���������: "
    };
    List<string> NameZina = new List<string>() 
    {
        "",
        "����", "",
        "",
        "����",""
    };
    List<string> textTeacher = new List<string>()
    {
        "��� �� ����� ������? ����� ���-�� �����? ",
        "", "����� ��� ������?! �������� ������� � ���� ���! ����� ����� � ���, ��� ��������� �����!",
        "� ���� ��� ������� ������ ����������, ��� � ���������, ��� �� ��� �� ������� ������!",
        "","� ������ ���� ����� � ������� ����! ����� � ��� �� �����. " +
        "���� ���� ����� �� �����, �� ������ ������ ����������� ������-��."
    };
    List<string> textZina = new List<string>()
    {
        "",
        "������������. � ���, ��������, ��� ������� ����� ������?",
        "","",
        "��, � �� ���� � ���. ������� ���� �� 76� ���������!",
        ""
    };
    List<string> text_Name = new List<string>()
    {
        "�������� ���������:","�������� ���������:"
    };
    List<string> text_0 = new List<string>()
    {
       "��, ����.","� ��� � �����, ��� �������, ��� ��, �� ����������. ���������� � ���� ���� �����. ��� ���� � �������� �� 78� ���������!"
    };
    List<string> text_1_2 = new List<string>()
    {
        "��, ����.","�� ���� �� ����������, ��? �� ��� ����, �������, ����� ��������� ��������� ��� ��������. �� �������, ��� ���� � �������� �� 78� ���������! "
    };
    List<string> text_3 = new List<string>()
    {
        "���! ������������ ��������� ��� ������ ��������, ��� ��. ��� ������. ������� ����������� � ����� �����! ",
        "���� �������� ��� 30 ����������� � �������� ������! ��� ���� � �������� �� 78� ���������!"
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
