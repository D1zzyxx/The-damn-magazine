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

    #region ����
    public TextMeshProUGUI line; // ������ �� TMPUGI ��� ������ ������
    public Button buttonNext; // ������ �� ������ "�����"
    public GameObject panelDialog; // ������ �� ������ �������

    private int indexLine; // ������ ������� ������ �������
    private bool isOrder; // ������ ����������� (�������������/�����)
    private bool isSecondDialogue = false; // ���� ��� ������� �������
    #endregion

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

    #region ������ Start � OnTriggerEnter2D
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
    #endregion

    #region ������ ������ �������
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
        buttonNext.interactable = true; // �������� ������
    }
    #endregion

    #region ������ ����������� �������
    // ����� ��� ��������� ��������� ������ �������
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
        else if (isSecondDialogue)
        {
            // ������ ������
            if (indexLine / 1 >= dialogTeacherSecond.Length || indexLine / 1 >= dialogPlayerSecond.Length)
            {
                EndSecondDialogue(); // ��������� ������ ������
                return;
            }

            // ����������, ��� ������� ��������
            if (isOrder == true)
            {
                line.text = dialogTeacherSecond[indexLine / 1];
                isOrder = false;
            }
            else if (isOrder == false)
            {
                line.text = dialogPlayerSecond[indexLine / 1];
                isOrder = true;
                indexLine++;
            }
        }
    }
    #endregion

    #region ������ ���������� �������
    // ����� ��� ���������� ������� �������
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // ��������� ������ �������
        buttonNext.interactable = false; // ��������� ������ "�����"
        Debug.Log("������ ������ ��������. ����� �������� ���������.");

        // ���� QuizManager �� ����� � ��������� ���������
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
        Debug.Log("������ ������ ��������. ������� ������ �������.");
    }
    #endregion
}