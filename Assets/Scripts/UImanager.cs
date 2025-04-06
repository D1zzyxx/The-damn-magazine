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
    [SerializeField] private TMP_Text InputText, QuestionText; //���������� ���: ������ �������, ��������� ������
    [SerializeField] private TMP_InputField inputField; //���������� ��� ��������� ��������� ������ � ���� ������
    [SerializeField] private string MyText; //���-��
    public GameObject panels;
    public Button continues; //��
    string inputText;
    int index = 0;
    int comparies = 0;
    List<string> managers = new List<string>() {"������� ����", "������� ������ �������", "����� ������� �������� \"������ ���� �������\"?" };
    private void Awake()
    {
        QuestionText.text = managers[index];
        
    }
    private void Update()
    {
        continues.onClick.AddListener(SaveInputText);
    }
    
    public void SaveInputText() //������� ��������� ��������� ������
    {
        if(index >= managers.Count)
        {
           
        }
        if (index == 0) Question1();
        else if (index == 1) Question2();
        else if (index == 2) Question3();
        
    }   
    private void Question1() //��������� 1 ������
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
    private void Question2()//��������� 2 ������
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
    private void Question3()//��������� 3 ������
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
