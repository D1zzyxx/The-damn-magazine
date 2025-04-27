using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    /*
     * Данный скрипт управляет:
     * Управляет последовательностью диалогов,
     * Отображает текстовые строки UI,
     * Переключается между репликами при нажатии кнопки <<Далее>>
     * Заканчивает диалог
     */

    #region Поля
    public GameObject player; // Ссылка на игрока, далее у него будем ВКЛ/ВЫКЛ Ходьбу
    Player playerMove;

    public TextMeshProUGUI line; // Ссылка на TMPUGI для вывода строки
    public Button buttonNext; // Ссылка на кнопку "Далее"
    public GameObject panelDialog; // Ссылка на панель диалога

    private int indexLine; // Индекс текущей строки диалога
    private bool isOrder; // Флажок очередности (преподаватель/игрок)
    private bool isSecondDialogue = false; // Флаг для второго диалога
    private bool isDialogueLost; // Флаг о том проигрывался ли диалог
    #endregion

    #region Реплики персонажей
    // Первый диалог
    private string[] dialogTeacherFirst = {
        "Здравствуй, Зина! Зачем пришла? Тебе что-то нужно?",
        "Нет, у меня нет вашего журнала. Зачем он тебе?",
        "Очень трогательно. Если тебе так интересно узнать свои оценки по моему предмету, давай я посмотрю в своем журнале.",
        "Так… Смотрю, ты не сдавала тест по «Мастеру и Маргарите» и «Войне и миру». " +
        "Думаю, тебе стоит не искать журнал для просмотра оценок. " +
        "Как никак, чтобы смотреть на оценки для начала нужно получить их."
    };

    private string[] dialogPlayerFirst = {
        "Здравствуйте! Я тут ищу журнал нашей группы. Вы случайно его не видели? Может, он у вас?",
        "Как жаль, что у вас нет нашего журнала! Я уже столько лет и зим ищу его. По ощущениям, 10 лет прошло! Он будто провалился сквозь землю, и я так скучаю по нему… А я же просто хочу узнать свои оценки!",
        "Правда? Спасибо большое!",
        "..."
    };

    // Второй диалог
    private string[] dialogTeacherSecond = {
        "Ты ужасно справилась. Но я не буду злиться. В последний раз помогаю бездарю! " +
        "Держи литературу. Прочитай ее внимательно, подготовься как следует, и в следующий раз придешь пересдавать."
    };

    private string[] dialogPlayerSecond = {
        "Тест по «Мастер и Маргарита», вообще, возможно сдать идеально? Что за «Миссия невыполнима»?"
    };
    #endregion

    #region Методы Start и OnTriggerEnter2D
    private void Start()
    {
        isDialogueLost = false;
        panelDialog.SetActive(false); // Отключаем диалоговую панель при старте
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        isSecondDialogue = false; // Начинаем с первого диалога
        playerMove = player.GetComponent<Player>();
        if(playerMove!=null)
        {
            Debug.LogError("Компонент не получен!");
        }
    }

    // Запускается при соприкосновении с коллайдером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isDialogueLost == false)
        {
            // Запускаем первый диалог
            StartFirstDialogue();

            // Переключаем флажок на true так как диалог начал воспроизводиться
            isDialogueLost = true;
        }
    }
    #endregion

    #region Методы старта диалога
    // Метод для запуска первого диалога
    public void StartFirstDialogue()
    {
        isSecondDialogue = false; // Устанавливаем флаг для первого диалога
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        panelDialog.SetActive(true); // Включаем панель диалога
        ActivateDialogue(); // Показываем первую строку диалога

       // Player playerMove = player.GetComponent<Player>(); // Получаем скрипт для дальнейших манипуляций
        playerMove.enabled = false; //Отключаем ходьбу

    }

    // Метод для запуска второго диалога
    public void StartSecondDialogue()
    {
        isSecondDialogue = true; // Устанавливаем флаг для второго диалога
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        panelDialog.SetActive(true); // Включаем панель диалога
        ActivateDialogue(); // Показываем первую строку диалога
        buttonNext.interactable = true; // Включаем кнопку
    }
    #endregion

    #region Логика очередности диалога
    // Метод для активации следующей строки диалога
    public void ActivateDialogue()
    {
        // Проверяем, какой диалог сейчас активен
        if (!isSecondDialogue)
        {
            // Первый диалог
            if (indexLine / 1 >= dialogTeacherFirst.Length || indexLine / 1 >= dialogPlayerFirst.Length)
            {
                EndFirstDialogue(); // Завершаем первый диалог
                return;
            }

            // Определяем, чья очередь говорить
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
            // Второй диалог
            if (indexLine / 1 >= dialogTeacherSecond.Length || indexLine / 1 >= dialogPlayerSecond.Length)
            {
                EndSecondDialogue(); // Завершаем второй диалог
                return;
            }

            // Определяем, чья очередь говорить
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

    #region Методы завершения диалога
    // Метод для завершения первого диалога
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // Выключаем панель диалога
        buttonNext.interactable = false; // Выключаем кнопку "Далее"
        Debug.Log("Первый диалог завершен. Можно начинать викторину.");

        // Ищем QuizManager на сцене и запускаем викторину
        QuizManager quizManager = FindObjectOfType<QuizManager>();

        if (quizManager != null)
        {
            quizManager.StartQuiz(); // Запускаем викторину
        }
    }

    // Метод для завершения второго диалога
    private void EndSecondDialogue()
    {
        panelDialog.SetActive(false); // Отключаем панель диалога
        buttonNext.interactable = false; // Деактивируем кнопку "Далее"
        Debug.Log("Второй диалог завершен. Предмет должен выпасть.");

       // Player playerMove = player.GetComponent<Player>(); // Получаем компонент для дальнейших манипуляций
        Debug.Log("компонент игрока получен");
        playerMove.enabled = true; //Включаем ходьбу по окончанию диалога
    }
    #endregion
}