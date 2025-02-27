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

    public TextMeshProUGUI line; // Ссылка на TMPUGI для вывода строки
    public Button buttonNext; // Ссылка на кнопку "Далее"
    public GameObject panelDialog; // Ссылка на панель диалога

    private int indexLine; // Индекс текущей строки диалога
    private bool isOrder; // Флажок очередности (преподаватель/игрок)
    private bool isSecondDialogue = false; // Флаг для второго диалога

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

    private void Start()
    {
        panelDialog.SetActive(false); // Отключаем диалоговую панель при старте
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        isSecondDialogue = false; // Начинаем с первого диалога
    }

    // Запускается при соприкосновении с коллайдером
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Запускаем первый диалог
            StartFirstDialogue();
        }
    }

    // Метод для запуска первого диалога
    public void StartFirstDialogue()
    {
        isSecondDialogue = false; // Устанавливаем флаг для первого диалога
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        panelDialog.SetActive(true); // Включаем панель диалога
        ActivateDialogue(); // Показываем первую строку диалога
    }

    // Метод для запуска второго диалога
    public void StartSecondDialogue()
    {
        isSecondDialogue = true; // Устанавливаем флаг для второго диалога
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        panelDialog.SetActive(true); // Включаем панель диалога
        ActivateDialogue(); // Показываем первую строку диалога
    }

    // Метод для активации следующей строки диалога
    public void ActivateDialogue()
    {
        // Проверяем, какой диалог сейчас активен
        if (!isSecondDialogue)
        {
            // Первый диалог
            if (indexLine / 2 >= dialogTeacherFirst.Length || indexLine / 2 >= dialogPlayerFirst.Length)
            {
                EndFirstDialogue(); // Завершаем первый диалог
                return;
            }

            // Определяем, чья очередь говорить
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
            // Второй диалог
            if (indexLine / 2 >= dialogTeacherSecond.Length || indexLine / 2 >= dialogPlayerSecond.Length)
            {
                EndSecondDialogue(); // Завершаем второй диалог
                return;
            }

            // Определяем, чья очередь говорить
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

    // Метод для завершения первого диалога
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // Отключаем панель диалога
        buttonNext.interactable = false; // Деактивируем кнопку "Далее"
        Debug.Log("Первый диалог завершен. Можно начинать викторину.");

        // Здесь можно вызвать метод для начала викторины из QuizManager
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
        Debug.Log("Второй диалог завершен. Предмет может выпасть.");
    }
}