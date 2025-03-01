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
        if (!PlayerPrefs.HasKey("Hearts")) HP = 5f; //��������� ������� ����� "Hearts". ��� ��� ����������, HP ����� ����� 5
        else PlayerPrefs.GetFloat("Hearts", HP);  // ����� HP ����� ������, ������� ���� ��������� �����
    }
    private void FixedUpdate()
    {
        if (HP ==0) //��� ������� �� ��������� �� ����� �����
        {
            YouLose();
        }
    }
    void OnCollisionEnter2D(Collision2D collision) //��� �������������� � ����� Door, ���������� ������ ���������� ������
    {
        if (collision.gameObject.tag == "Door") //��� �������������� � ����� Door, ���������� ������ ���������� ������
        {
            CloseScene();
        }
        if (collision.gameObject.tag == "Block") //��� �������������� � ����� Block, ����������� ���������� ������
        {
            HP -= 1;
            Bar.fillAmount = HP / 5;
        }
    }
    void CloseScene()
    {
        PlayerPrefs.SetFloat("Hearts", HP); //���������� � ����� "Hearts" ������ � ���������� ������
    }
    public void YouLose() //������� �� ������ �����
    {
        SceneManager.LoadScene(2);
    }
}
 