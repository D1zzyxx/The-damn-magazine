using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int check = 0;
    public TextMeshProUGUI TMPUGUI;

    //������ ��� ������ ������������� � �������� ��������!
    //�� �� ����� � ���� ������� ��������� ��������
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            check++;
            Debug.Log("����� ������� ������ �� �������� ��������� � �����������");
            Debug.Log($"���������� ������: {check}");

            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (TMPUGUI != null)
        {
            TMPUGUI.text = "������: " + check.ToString();
        }
    }
}
