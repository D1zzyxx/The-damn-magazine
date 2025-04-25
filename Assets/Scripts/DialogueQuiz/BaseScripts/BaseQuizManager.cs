using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseQuizManager : MonoBehaviour
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

    private BaseDialogueManager dialogueManager;

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

    public abstract class Question
    {
        public string questionText; //����� �������
        public string[] options; //������ ��������� �������
        public int correctAnswerIndex; //������ ����������� ������ (-1, ���� ����������� ������ ���)

        protected Question(string text, string[] opts, int correctIndex)
        {
            questionText = text;
            options = opts;
            correctAnswerIndex = correctIndex;
        }
    }
    protected abstract Question[] questions { get; }
    protected abstract void OnQuizEnd(); // ������ ���������� (��������, ������ �������)

    // ����� ��� ������ ���������
    public virtual void StartQuiz()
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
    public virtual void DisplayQuestion()
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
    public virtual void CheckAnswer(int selectedAnswerIndex)
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
    public virtual void EndQuiz()
    {
        panelQuiz.SetActive(false); // ��������� ������ ���������

        Debug.Log($"��������� ���������! ���������� �������: {correctAnswersCount} �� {questions.Length}");
    }

    // ����� ��� ������� ������� ������� � ��������� ��������
    public virtual void StartDialogueAndDropItem()
    {
        // ��������� ������ ���������
        panelQuiz.SetActive(false);

        // ��������� ������ ������ ����� DialogueManager
        if (dialogueManager != null)
        {
            dialogueManager.StartSecondDialogue(); // ��������������, ��� DialogueManager �������� ����� StartSecondDialogue()
        }

        // ��������� ��������
        DropItem();
    }

    // ����� ��� ��������� ��������
    public virtual void DropItem()
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
