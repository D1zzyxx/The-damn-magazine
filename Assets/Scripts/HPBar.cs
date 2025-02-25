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
        if (!PlayerPrefs.HasKey("Hearts")) HP = 5f; //��������� ������� ����� "Hearts". ��� ��� ����������, HP ����� ����� 5
        else PlayerPrefs.GetFloat("Hearts", HP);  // ����� HP ����� ������, ������� ���� ��������� �����
    }
    private void FixedUpdate()
    {
        
    }
    void OnCollisionEnter2D(Collision2D colDoor) //��� �������������� � ����� Door, ���������� ������ ���������� ������
    {
        if (colDoor.gameObject.tag == "Door")
        {
            CloseScene();
        }
    }
    void CloseScene()
    {
        PlayerPrefs.SetFloat("Hearts", HP); //���������� � ����� "Hearts" ������ � ���������� ������
    }
}
 