using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class QuizManager : MonoBehaviour
{
    /* ������ ������ ����������:
     * ��������� ������������������� ��������
     * ���������� ������� ������ � �������� �������
     * ��������� ��������� ������� ������
     * ������������ ���������� ������
     * ��������� ��������� ����� ���� ��������
     */

    /*�� ������ ����� ������ ������ 
     * ����������� �����
     * �� ������� ������ �� ������ �����������������
     * 
     * � ������ ���:
     *  ����������� ����� (���������) 
     *  ��� ����������� ����������
     *  ���������� ������ ���������� ������
     *  � ����������� ������ � ���������� ������
     */

    #region ���������� (����)
    private int currentQuestIndex; // ������ �������� �������
    private int correctAnswersCount; // ������� ���������� �������
    private bool isQuizPassed; // ���� ����������� ���������

    public TextMeshProUGUI lineQuestion; // ������ �� TMP ��� ����������� �������
    public GameObject panelQuiz; // ������ ���������
    public Button btnReply1; // ������ ������� ������
    public Button btnReply2; // ������ ������� ������
    public Button btnReply3; // ������ �������� ������

    public TextMeshProUGUI btnAnswerText1; // ����� ������ ������� ������
    public TextMeshProUGUI btnAnswerText2; // ����� ������ ������� ������
    public TextMeshProUGUI btnAnswerText3; // ����� ������ �������� ������

    public GameObject itemPrefab; // Prefab �������� (������������ ����� � ����������)
    #endregion

    #region ������ ��������
    private class Question
    {
        public string questionText; //����� �������
        public string[] options; //������ ��������� �������
        public int correctAnswerIndex; //������ ����������� ������ (-1, ���� ����������� ������ ���)

        public Question(string text, string[] opts, int correctIndex)
        {
            questionText = text;
            options = opts;
            correctAnswerIndex = correctIndex;
        }
    }

    private Question[] questions =
    {
        new Question("��� �������� ������� ������ ������ � ���������?",
            new string[] { "������ ��������", "����� �����������", "��� �������" }, 0),
        new Question("����� ���� ������������ ���������, ����� ������������� ����� �������?",
            new string[] {"���� ��������", "���� �������", "���� ��������"}, 0),
        new Question("������� ���� � ������ ��������� ������������ ���������?",
            new string[] {"2", "4" , "6"}, 1),
        new Question("���, �� ������ �������, ��������� ���������?",
            new string[] {"������", "������", "��������� ������"}, 2),
        new Question("��� ���������������� ����� ����� � ������ �� ��������� � ������������ ���������� ������� ������?",
            new string[] {"�� ����� ����������� � ������������", 
                          "�� ����� ����������� � ������������", 
                          "�� ����� ����������� � ������������"}, 3), // ��� ����������� ������, ��� ��� ������ ����������� ������ 3 (0, 1, 2, 3),
                                                                      // ����� ������������ ������ ������ � ������ 2, ��� �� �������, ��� ��� ����������� ������
                                                                      // � �������� � ��� �� ���������� ��������, �������� �� ������ �������, �� ��������
                                                                      // ���� qwen chat ������� ��� ����� ������������ ������ -1 ��� ��� �� ������ ������� ������
        new Question("��� �������� ������� ������ ����� � ���",
            new string[] { "��� �������", "����� �����������", "���� ��������" }, 0),
        new Question("�������� ������� ������ ������ ����� � ���",
            new string[] {"������ ����������, ������ �������, ���� �������",
                          "������� ������, ������� ������, �������� �������",
                          "������ ������������, ���� �����������, �������� ��������"}, 0),
        new Question("����� ������������ ������� ����������� � ������?",
            new string[] {"������������� ����� 1812 ����", "�������� ����� 1853-1856 �����", "������-�������� ����� 1877-1878 �����"}, 0),
        new Question("��� � ������ ������������ �������� ��������� � ������� ��������?",
            new string[] {"����� ������������������ �������� � ���������",
                         "����� ��������� ����� �������� � ������ ��������",
                         "����� ������ ��������� ������� ������� �� �����"}, 1),
        new Question("����� ������� ������� ���������� ������� ���������� ��������� � ����������� �� ��������� � ��������� �������� �����?",
            new string[] {"����� ��������� ��������� � ����� � ������� ������",
                              "����� �������� ��������� ���������� �����",
                              "����� ������������� ������������� ����� ��������"}, 1)
    };
    #endregion

    private void Start()
    {
        isQuizPassed = false;
        panelQuiz.SetActive(false); // ��������� ������ ��������� ��� ������
    }

    // ����� ��� ������ ���������
    public void StartQuiz()
    {
        /*
         * �� ������ ����� ������� ����� ������
         * ����� �� �� ����� ����� ��� ��������� ���������
         * � ��� �� ���������������� ��� ��������
         */
        if (!isQuizPassed)
        {
            currentQuestIndex = 0; // �������� � ������� �������
            correctAnswersCount = 0; // �������� ������� ���������� �������
            panelQuiz.SetActive(true); // �������� ������ ���������
            DisplayQuestion(); // ���������� ������ ������
            isQuizPassed = true; // ������ ������ ����, ��� ��������� ��������
        }
    }

    // ����� ��� ����������� �������� �������
    private void DisplayQuestion()
    {
        // ���������, �� ����������� �� �������
        if (currentQuestIndex >= questions.Length)
        {
            EndQuiz(); // ��������� ���������
            return;
        }

        // �������� ������� ������
        Question currentQuestion = questions[currentQuestIndex];

        // ������������� ����� �������
        lineQuestion.text = currentQuestion.questionText;

        // ��������� ����� ������
        if (currentQuestion.options.Length > 0)
            btnAnswerText1.GetComponent<TMP_Text>().text = currentQuestion.options[0];
        else
            btnReply1.interactable = false;

        if (currentQuestion.options.Length > 1)
            btnAnswerText2.GetComponent<TMP_Text>().text = currentQuestion.options[1];
        else
            btnReply2.interactable = false;

        if (currentQuestion.options.Length > 2)
            btnAnswerText3.GetComponent<TMP_Text>().text = currentQuestion.options[2];
        else
            btnReply3.interactable = false;
    }

    // ����� ��� �������� ������
    public void CheckAnswer(int selectedAnswerIndex)
    {
        // �������� ������� ������
        Question currentQuestion = questions[currentQuestIndex];

        // ���� ������ ����� ���������� �����
        if (currentQuestion.correctAnswerIndex != -1)
        {
            // ��������� ������������ ������
            if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
            {
                correctAnswersCount++; // ����������� ������� ���������� �������
                Debug.Log("���������� �����!");
            }
            else
            {
                Debug.Log("������������ �����.");
            }
        }
        else
        {
            // ��� �������� ��� ����������� ������
            Debug.Log("�� ���� ������ ��� ����������� ������. ����������...");
        }

        // ��������� � ���������� ������� ��� ��������� ���������
        currentQuestIndex++;

        if (currentQuestIndex >= questions.Length)
        {
            EndQuiz(); // ��������� ���������
            StartDialogueAndDropItem(); // ��������� ������ ������ � ��������� ��������
        }
        else
        {
            DisplayQuestion(); // ���������� ��������� ������
        }
    }

    // ����� ��� ���������� ���������
    private void EndQuiz()
    {
        panelQuiz.SetActive(false); // ��������� ������ ���������

        Debug.Log($"��������� ���������! ���������� �������: {correctAnswersCount} �� {questions.Length}");
    }

    // ����� ��� ������� ������� ������� � ��������� ��������
    private void StartDialogueAndDropItem()
    {
        // ��������� ������ ���������
        panelQuiz.SetActive(false);

        // ��������� ������ ������ ����� DialogueManager
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.StartSecondDialogue(); // ��������������, ��� DialogueManager �������� ����� StartSecondDialogue()
        }

        // ��������� ��������
        DropItem();
    }

    // ����� ��� ��������� ��������
    private void DropItem()
    {
        // �������, ��� �������� �������

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        //������� ��������, ��� �� �������� �� ������ �� ������
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            Debug.Log("������� �����! ������");
        }
        else
        {
            Debug.Log("Prefab �������!");
        }
    }
}
