using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;

public class QuizManager : MonoBehaviour
{
    /* Данный скрипт занимается:
     * Управляет последовательностью вопросов
     * Отображает текущий вопрос и варианты ответов
     * Проверяет выбранные игроком ответы
     * Подсчитывает правильные ответы
     * Завершает викторину после всех вопросов
     */

    private int currentQuestIndex; // Индекс текущего вопроса
    private int correctAnswersCount; // Счетчик правильных ответов

    public TextMeshProUGUI lineQuestion; // Ссылка на TMP для отображения вопроса
    public GameObject panelQuiz; // Панель викторины
    public Button btnReply1; // Кнопка первого ответа
    public Button btnReply2; // Кнопка второго ответа
    public Button btnReply3; // Кнопка третьего ответа

    public GameObject itemPrefab; // Prefab предмета (назназначить нужно в инспекторе)

    #region Массив вопросов
    private class Question
    {
        
        
        public string questionText; // Текст вопроса
        public string[] options; // Массив вариантов ответов
        public int correctAnswerIndex; // Индекс правильного ответа (-1, если правильного ответа нет)

        public Question(string text, string[] opts, int correctIndex)
        {
            questionText = text;
            options = opts;
            correctAnswerIndex = correctIndex;
        }
    }

    private Question[] questions =
    {
        new Question("Кто является автором романа Мастер и Маргарита?",
            new string[] { "Михаил Булгаков", "Федор Достоевский", "Лев Толстой" }, 0),
        new Question("Какой крем использовала Маргарита, чтобы преобразиться перед полетом?",
            new string[] {"Крем Азазелло", "Крем Воланда", "Крем Бегемота"}, 0),
        new Question("Сколько глав в романе посвящено евангельским страницам?",
            new string[] {"2", "4" , "6"}, 1),
        new Question("Что, по мнению Воланда, испортило москвичей?",
            new string[] {"Деньги", "Власть", "Квартиный вопрос"}, 2),
        new Question("Как трансформируется образ Иешуа в романе по сравнению с традиционным библейским образом Иисуса?",
            new string[] {"Он более агрессивный и воинственный", "", ""}, -1), // Нет правильного ответа
        new Question("Кто является автором романа Война и мир",
            new string[] { "Лев Толстой", "Федор Достоевский", "Иван Тургенев" }, 0),
        new Question("Назовите главных героев романа Война и мир",
            new string[] {"Андрей Болконский, Наташа Ростова, Пьер Безухов",
                          "Евгений Онегин, Татьяна Ларина, Владимир Ленский",
                          "Родион Раскольников, Соня Мармеладова, Порфирий Петрович"}, 0),
        new Question("Какие исторические события описываются в романе?",
            new string[] {"Отечественная война 1812 года", "Крымская война 1853-1856 годов", "Русско-турецкая война 1877-1878 годов"}, 0),
        new Question("Как в романе раскрывается проблема истинного и ложного героизма?",
            new string[] {"Через противопоставление Кутузова и Наполеона",
                         "Через сравнение Пьера Безухова и Федора Долохова",
                         "Через анализ поведения Николая Ростова на войне"}, 1),
        new Question("Каким образом Толстой показывает влияние кризисного положения в государстве на характеры и моральные ценности людей?",
            new string[] {"Через изменение отношения к войне у главных героев",
                              "Через описание разорения дворянских семей",
                              "Через трансформацию мировоззрения Пьера Безухова"}, 1)
    };
    #endregion

    private void Start()
    {
        panelQuiz.SetActive(false); // Отключаем панель викторины при старте
    }

    // Метод для начала викторины
    public void StartQuiz()
    {
        currentQuestIndex = 0; // Начинаем с первого вопроса
        correctAnswersCount = 0; // Обнуляем счетчик правильных ответов
        panelQuiz.SetActive(true); // Включаем панель викторины
        DisplayQuestion(); // Показываем первый вопрос
    }

    // Метод для отображения текущего вопроса
    private void DisplayQuestion()
    {
        // Проверяем, не закончились ли вопросы
        if (currentQuestIndex >= questions.Length)
        {
            EndQuiz(); // Завершаем викторину
            return;
        }

        // Получаем текущий вопрос
        Question currentQuestion = questions[currentQuestIndex];

        // Устанавливаем текст вопроса
        lineQuestion.text = currentQuestion.questionText;

        // Заполняем текст кнопок
        if (currentQuestion.options.Length > 0)
            btnReply1.GetComponent<TMP_Text>().text = currentQuestion.options[0];
        else
            btnReply1.interactable = false;

        if (currentQuestion.options.Length > 1)
            btnReply2.GetComponent<TMP_Text>().text = currentQuestion.options[1];
        else
            btnReply2.interactable = false;

        if (currentQuestion.options.Length > 2)
            btnReply3.GetComponent<TMP_Text>().text = currentQuestion.options[2];
        else
            btnReply3.interactable = false;
    }

    // Метод для проверки ответа
    public void CheckAnswer(int selectedAnswerIndex)
    {
        // Получаем текущий вопрос
        Question currentQuestion = questions[currentQuestIndex];

        // Если вопрос имеет правильный ответ
        if (currentQuestion.correctAnswerIndex != -1)
        {
            // Проверяем правильность ответа
            if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
            {
                correctAnswersCount++; // Увеличиваем счетчик правильных ответов
                Debug.Log("Правильный ответ!");
            }
            else
            {
                Debug.Log("Неправильный ответ.");
            }
        }
        else
        {
            // Для вопросов без правильного ответа
            Debug.Log("На этот вопрос нет правильного ответа. Продолжаем...");
        }

        // Переходим к следующему вопросу или завершаем викторину
        currentQuestIndex++;

        if (currentQuestIndex >= questions.Length)
        {
            EndQuiz(); // Завершаем викторину
            StartDialogueAndDropItem(); // Запускаем второй диалог и выпадение предмета
        }
        else
        {
            DisplayQuestion(); // Показываем следующий вопрос
        }
    }

    // Метод для завершения викторины
    private void EndQuiz()
    {
        panelQuiz.SetActive(false); // Отключаем панель викторины
        Debug.Log($"Викторина завершена! Правильных ответов: {correctAnswersCount} из {questions.Length}");
    }

    // Метод для запуска второго диалога и выпадения предмета
    private void StartDialogueAndDropItem()
    {
        // Отключаем панель викторины
        panelQuiz.SetActive(false);

        // Запускаем второй диалог через DialogueManager
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager != null)
        {
            dialogueManager.StartSecondDialogue(); // Предполагается, что DialogueManager содержит метод StartSecondDialogue()
        }

        // Выпадение предмета
        DropItem();
    }

    // Метод для выпадения предмета
    private void DropItem()
    {
        // Позиция, где появится предмет

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        //Создаем проверку, так же проверка не пустая ли ссылка
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            Debug.Log("Предмет выпал!");
        }
        else
        {
            Debug.Log("Prefab предмета не назначен!");
        }
    }
}