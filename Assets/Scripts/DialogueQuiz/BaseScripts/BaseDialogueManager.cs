using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseDialogueManager : MonoBehaviour
{
    // ����� ����
    public GameObject player; // ������ �� ������
    public TextMeshProUGUI line; // ����� �������
    public Button buttonNext; // ������ "�����"
    public GameObject panelDialog; // ������ �������

    protected int indexLine; // ������ ������� ������
    protected bool isOrder = true; // ��� �������: ������ (true) ��� ����� (false)
    protected bool isSecondDialogue; // ���� ��� ������� �������

    // ����������� �������� ��� ������ (���������� �� ���������)
    protected abstract string[] TeacherFirstLines { get; }
    protected abstract string[] PlayerFirstLines { get; }
    protected abstract string[] TeacherSecondLines { get; }
    protected abstract string[] PlayerSecondLines { get; }

    // ����������� ����� ���������� (����������� � �����������)
    protected abstract void OnDialogueEnd();

    // ����� ������
    public virtual void StartFirstDialogue()
    {
        indexLine = 0;
        isSecondDialogue = false;
        isOrder = true; // ������ ��������
        panelDialog.SetActive(true);
        DisablePlayerMovement(); // ��������� ������

        // ���������� ������ �������
        ActivateDialogue();
    }

    public virtual void StartSecondDialogue()
    {
        indexLine = 0;
        isSecondDialogue = true;
        isOrder = true; // ������ �������� ������ ������
        panelDialog.SetActive(true);
        DisablePlayerMovement();

        // ���������� ������ ������� ������� �������
        ActivateDialogue();
    }

    public virtual void ActivateDialogue()
    {
        if (IsDialogueFinished())
        {
            EndDialogue();
            return;
        }

        // ����������, ��� ������� ��������
        if (isOrder)
            line.text = GetTeacherLine();
        else
            line.text = GetPlayerLine();

        isOrder = !isOrder; // ����������� �������
    }

    public virtual bool IsDialogueFinished()
    {
        int maxLines = GetMaxLines();
        return indexLine >= maxLines;
    }

    public virtual int GetMaxLines()
    {
        if (!isSecondDialogue)
            return Mathf.Min(TeacherFirstLines.Length, PlayerFirstLines.Length);
        else
            return Mathf.Min(TeacherSecondLines.Length, PlayerSecondLines.Length);
    }

    public virtual string GetTeacherLine()
    {
        string[] lines = isSecondDialogue ? TeacherSecondLines : TeacherFirstLines;
        return lines[indexLine];
    }

    public virtual string GetPlayerLine()
    {
        string[] lines = isSecondDialogue ? PlayerSecondLines : PlayerFirstLines;
        return lines[indexLine++];
    }

    public virtual void EndDialogue()
    {
        panelDialog.SetActive(false);
        buttonNext.interactable = false;
        EnablePlayerMovement(); // ������������ ������
        OnDialogueEnd(); // �������� ���������� ������ ����������
    }

    public virtual void DisablePlayerMovement()
    {
        Player playerMove = player.GetComponent<Player>(); // �������� ������ ��� ���������� �����������
        playerMove.enabled = false; //��������� ������
    }

    public virtual void EnablePlayerMovement()
    {
        Player playerMove = player.GetComponent<Player>(); // �������� ������ ��� ���������� �����������
        playerMove.enabled = true; //��������� ������
    }
}