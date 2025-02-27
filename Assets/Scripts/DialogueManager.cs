using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    /*
     * ������ ������ ���������:
     * ��������� ������������������� ��������,
     * ���������� ��������� ������ UI,
     * ������������� ����� ��������� ��� ������� ������ <<�����>>
     * ����������� ������
     */

    public TextMeshProUGUI line; // ������ �� TMPUGI ��� ������ ������
    public Button buttonNext; // ������ �� ������ "�����"
    public GameObject panelDialog; // ������ �� ������ �������

    private int indexLine; // ������ ������� ������ �������
    private bool isOrder; // ������ ����������� (�������������/�����)
    private bool isSecondDialogue = false; // ���� ��� ������� �������

    #region ������� ����������
    // ������ ������
    private string[] dialogTeacherFirst = {
        "����������, ����! ����� ������? ���� ���-�� �����?",
        "���, � ���� ��� ������ �������. ����� �� ����?",
        "����� �����������. ���� ���� ��� ��������� ������ ���� ������ �� ����� ��������, ����� � �������� � ����� �������.",
        "��� ������, �� �� ������� ���� �� �������� � ��������� � ������ � ����. " +
        "�����, ���� ����� �� ������ ������ ��� ��������� ������. " +
        "��� �����, ����� �������� �� ������ ��� ������ ����� �������� ��."
    };

    private string[] dialogPlayerFirst = {
        "������������! � ��� ��� ������ ����� ������. �� �������� ��� �� ������? �����, �� � ���?",
        "��� ����, ��� � ��� ��� ������ �������! � ��� ������� ��� � ��� ��� ���. �� ���������, 10 ��� ������! �� ����� ���������� ������ �����, � � ��� ������ �� ���� � � �� ������ ���� ������ ���� ������!",
        "������? ������� �������!",
        "..."
    };

    // ������ ������
    private string[] dialogTeacherSecond = {
        "�� ������ ����������. �� � �� ���� �������. � ��������� ��� ������� �������! " +
        "����� ����������. �������� �� �����������, ����������� ��� �������, � � ��������� ��� ������� �����������."
    };

    private string[] dialogPlayerSecond = {
        "���� �� ������� � ���������, ������, �������� ����� ��������? ��� �� ������� �����������?"
    };
    #endregion

    private void Start()
    {
        panelDialog.SetActive(false); // ��������� ���������� ������ ��� ������
        indexLine = 0; // �������� ������
        isOrder = true; // ������ ������� �������������
        isSecondDialogue = false; // �������� � ������� �������
    }

    // ����������� ��� ��������������� � �����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // ��������� ������ ������
            StartFirstDialogue();
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
    }

    // ����� ��� ������� ������� �������
    public void StartSecondDialogue()
    {
        isSecondDialogue = true; // ������������� ���� ��� ������� �������
        indexLine = 0; // �������� ������
        isOrder = true; // ������ ������� �������������
        panelDialog.SetActive(true); // �������� ������ �������
        ActivateDialogue(); // ���������� ������ ������ �������
    }

    // ����� ��� ��������� ��������� ������ �������
    public void ActivateDialogue()
    {
        // ���������, ����� ������ ������ �������
        if (!isSecondDialogue)
        {
            // ������ ������
            if (indexLine / 2 >= dialogTeacherFirst.Length || indexLine / 2 >= dialogPlayerFirst.Length)
            {
                EndFirstDialogue(); // ��������� ������ ������
                return;
            }

            // ����������, ��� ������� ��������
            if (isOrder == true)
            {
                line.text = dialogTeacherFirst[indexLine / 2];
                isOrder = false;
            }
            else
            {
                line.text = dialogPlayerFirst[indexLine / 2];
                isOrder = true;
                indexLine++;
            }
        }
        else
        {
            // ������ ������
            if (indexLine / 2 >= dialogTeacherSecond.Length || indexLine / 2 >= dialogPlayerSecond.Length)
            {
                EndSecondDialogue(); // ��������� ������ ������
                return;
            }

            // ����������, ��� ������� ��������
            if (isOrder == true)
            {
                line.text = dialogTeacherSecond[indexLine / 2];
                isOrder = false;
            }
            else
            {
                line.text = dialogPlayerSecond[indexLine / 2];
                isOrder = true;
                indexLine++;
            }
        }
    }

    // ����� ��� ���������� ������� �������
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // ��������� ������ �������
        buttonNext.interactable = false; // ������������ ������ "�����"
        Debug.Log("������ ������ ��������. ����� �������� ���������.");

        // ����� ����� ������� ����� ��� ������ ��������� �� QuizManager
        QuizManager quizManager = FindObjectOfType<QuizManager>();
        if (quizManager != null)
        {
            quizManager.StartQuiz(); // ��������� ���������
        }
    }

    // ����� ��� ���������� ������� �������
    private void EndSecondDialogue()
    {
        panelDialog.SetActive(false); // ��������� ������ �������
        buttonNext.interactable = false; // ������������ ������ "�����"
        Debug.Log("������ ������ ��������. ������� ����� �������.");
    }
}