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
    int index;
    static bool chemistyQuest = false;
    int count = 0;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Hearts")) HP = 5f; //��������� ������� ����� "Hearts". ��� ��� ����������, HP ����� ����� 5
        else PlayerPrefs.GetFloat("Hearts", HP);  // ����� HP ����� ������, ������� ���� ��������� �����
        PlayerPrefs.Save();
    }
    private void FixedUpdate()
    {
        if (HP == 0) //��� ������� �� ��������� �� ����� �����
        {
            YouLose();
        }
        
    }
    private void Update()
    {
        chemistyQuest = UImanager.OpenQuests;
        if (chemistyQuest == true) 
        {
            ChemistryQuest();
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
    void ChemistryQuest()
    {
        if (count == 0)
        {
            index = PlayerPrefs.GetInt("Comparies");
            if (index == 0)
            {
                HP -= 3;
                Bar.fillAmount = HP / 5;
                Debug.Log("����� ��");
            }
            else if (index == 1)
            {
                HP -= 2;
                Bar.fillAmount = HP / 5;
            }
            else if (index == 2)
            {
                HP -= 1;
                Bar.fillAmount = HP / 5;
            }
            chemistyQuest = false;
            count++;
        }
    }
}
 