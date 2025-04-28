using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhysicsDialogue : MonoBehaviour
{
    /* ���� ����� ����� ��� �������
     * ��� � ������� ������� �������, � ������ � ������� ������� ���� ����
     * � � ������� ���� ���� �������, � �� ���� ��� ������������ ��-�� ������ ������
     * ������ ��� �����, �������� ����� � �� ����
     */

    public GameObject player; // ������ �� ������, ����� � ���� ����� ���/���� ������
    private Player playerMove;


    public TextMeshProUGUI line; // ������ �� TMPUGI ��� ������ ������
    public Button buttonNext; // ������ �� ������ "�����"
    public GameObject panelDialog; // ������ �� ������ �������

    private int indexLine; // ������ ������� ������ �������
    private bool isOrder; // ������ ����������� (�������������/�����)
    private bool isSecondDialogue = false; // ���� ��� ������� �������
    private bool isDialogueLost; // ���� � ��� ������������ �� ������

    private string[] dialogTeacherFirst = 
    {   "�������: ������������! �������, ����������, � ��� ��� �� �������� ������� ���� ������?",
        "������� ��� ����� �� ������ 10 �����." + "������������, ��� ��� ����� ���������",
        "�����, ���������� �����. � ���� ������ ��������, ����� ������ ��� �� �����." + "��������, ����� �����", 
    };

    private string[] dialogPlayerFirst =
    {
        "�������� ����������: �������, � ������ �����. ��� ��� 5 �����.",
        "���-���-���-���-���-���-���",
        "�������� ����������: ��, ������ �� ������ �������, ��? �����, ������, �� ������ ����������� �������!",
    };

    private void Start()
    {
        isDialogueLost = false;
        panelDialog.SetActive(false); // ��������� ���������� ������ ��� ������
        indexLine = 0; // �������� ������
        isOrder = true; // ������ ������� �������������
        isSecondDialogue = false; // �������� � ������� �������

        playerMove = player.GetComponent<Player>(); // �������� ������ ��� ���������� �����������
    }

    // ����������� ��� ��������������� � �����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isDialogueLost == false)
        {
            // ��������� ������ ������
            StartFirstDialogue();

            // ����������� ������ �� true ��� ��� ������ ����� ����������������
            isDialogueLost = true;
        }
    }

    // ����� ��� ������� ������� �������
    public void StartFirstDialogue()
    {
        isSecondDialogue = false; // ������������� ���� ��� ������� �������
        indexLine = 0; // �������� ������
        isOrder = true; // ������ ������� �������������
        panelDialog.SetActive(true); // �������� ������ �������
        ActivateDialogue(); // ���������� ������ ������ �������

        playerMove = player.GetComponent<Player>(); // �������� ������ ��� ���������� �����������
        playerMove.enabled = false; //��������� ������

    }

    public void ActivateDialogue()
    {
        // ���������, ����� ������ ������ �������
        if (!isSecondDialogue)
        {
            // ������ ������
            if (indexLine / 1 >= dialogTeacherFirst.Length || indexLine / 1 >= dialogPlayerFirst.Length)
            {
                EndFirstDialogue(); // ��������� ������ ������

                return;
            }

            // ����������, ��� ������� ��������
            if (isOrder == true)
            {
                line.text = dialogTeacherFirst[indexLine / 1];
                isOrder = false;
            }
            else if (isOrder == false)
            {
                line.text = dialogPlayerFirst[indexLine / 1];
                isOrder = true;
                indexLine++;
            }
        }
    }
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // ��������� ������ �������
        buttonNext.interactable = false; // ��������� ������ "�����"
        Debug.Log("������ ��������");
        playerMove.enabled = true;
    }
}
