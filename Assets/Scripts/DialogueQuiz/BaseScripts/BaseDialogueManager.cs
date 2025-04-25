using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseDialogueManager : MonoBehaviour
{
    // Общие поля
    public GameObject player; // Ссылка на игрока
    public TextMeshProUGUI line; // Текст диалога
    public Button buttonNext; // Кнопка "Далее"
    public GameObject panelDialog; // Панель диалога

    protected int indexLine; // Индекс текущей строки
    protected bool isOrder = true; // Кто говорит: препод (true) или игрок (false)
    protected bool isSecondDialogue; // Флаг для второго диалога

    // Абстрактные свойства для реплик (наследники их реализуют)
    protected abstract string[] TeacherFirstLines { get; }
    protected abstract string[] PlayerFirstLines { get; }
    protected abstract string[] TeacherSecondLines { get; }
    protected abstract string[] PlayerSecondLines { get; }

    // Абстрактный метод завершения (реализуется в наследниках)
    protected abstract void OnDialogueEnd();

    // Общая логика
    public virtual void StartFirstDialogue()
    {
        indexLine = 0;
        isSecondDialogue = false;
        isOrder = true; // Препод начинает
        panelDialog.SetActive(true);
        DisablePlayerMovement(); // Блокируем игрока

        // Отображаем первую реплику
        ActivateDialogue();
    }

    public virtual void StartSecondDialogue()
    {
        indexLine = 0;
        isSecondDialogue = true;
        isOrder = true; // Препод начинает второй диалог
        panelDialog.SetActive(true);
        DisablePlayerMovement();

        // Отображаем первую реплику второго диалога
        ActivateDialogue();
    }

    public virtual void ActivateDialogue()
    {
        if (IsDialogueFinished())
        {
            EndDialogue();
            return;
        }

        // Определяем, чья очередь говорить
        if (isOrder)
            line.text = GetTeacherLine();
        else
            line.text = GetPlayerLine();

        isOrder = !isOrder; // Переключаем очередь
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
        EnablePlayerMovement(); // Разблокируем игрока
        OnDialogueEnd(); // Вызываем уникальную логику завершения
    }

    public virtual void DisablePlayerMovement()
    {
        Player playerMove = player.GetComponent<Player>(); // Получаем скрипт для дальнейших манипуляций
        playerMove.enabled = false; //Отключаем ходьбу
    }

    public virtual void EnablePlayerMovement()
    {
        Player playerMove = player.GetComponent<Player>(); // Получаем скрипт для дальнейших манипуляций
        playerMove.enabled = true; //Отключаем ходьбу
    }
}