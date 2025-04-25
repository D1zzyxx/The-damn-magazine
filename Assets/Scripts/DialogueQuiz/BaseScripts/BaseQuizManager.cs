using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseQuizManager : MonoBehaviour
{
    /* Данный скрипт занимается:
     * Управляет последовательностью вопросов
     * Отображает текущий вопрос и варианты ответов
     * Проверяет выбранные игроком ответы
     * Подсчитывает правильные ответы
     * Завершает викторину после всех вопросов
     */

    /*На основе этого класса сделай 
     * Абстрактный класс
     * НО СНАЧАЛА ДОПИЛИ ДО ПОЛНОЙ РАБОТОСПОСОБНОСТИ
     * 
     * В ГОЛОВЕ КАК:
     *  абстрактный класс (заготовка) 
     *  все необходимые переменные
     *  констуктор класса содержащий ответы
     *  и необходимые методы с реализацие внутри
     */

    private BaseDialogueManager dialogueManager;

    #region Переменные (Поля)
    private int currentQuestIndex; // Индекс текущего вопроса
    private int correctAnswersCount; // Счетчик правильных ответов
    private bool isQuizPassed; // Флаг прохождения викторины

    public TextMeshProUGUI lineQuestion; // Ссылка на TMP для отображения вопроса
    public GameObject panelQuiz; // Панель викторины
    public Button btnReply1; // Кнопка первого ответа
    public Button btnReply2; // Кнопка второго ответа
    public Button btnReply3; // Кнопка третьего ответа

    public TextMeshProUGUI btnAnswerText1; // Текст кнопки первого ответа
    public TextMeshProUGUI btnAnswerText2; // Текст кнопки второго ответа
    public TextMeshProUGUI btnAnswerText3; // Текст кнопки третьего ответа

    public GameObject itemPrefab; // Prefab предмета (назназначить нужно в инспекторе)
    #endregion

    public abstract class Question
    {
        public string questionText; //Текст вопроса
        public string[] options; //Массив вариантов ответов
        public int correctAnswerIndex; //Индекс правильного ответа (-1, если правильного ответа нет)

        protected Question(string text, string[] opts, int correctIndex)
        {
            questionText = text;
            options = opts;
            correctAnswerIndex = correctIndex;
        }
    }
    protected abstract Question[] questions { get; }
    protected abstract void OnQuizEnd(); // Логика завершения (например, запуск диалога)

    // Метод для начала викторины
    public virtual void StartQuiz()
    {
        /*
         * По логике нужно сделать через флажок
         * Чтобы мы не могли много раз проходить викторину
         * И она не воспроизводилась при контакте
         */
        if (!isQuizPassed)
        {
            currentQuestIndex = 0; // Начинаем с первого вопроса
            correctAnswersCount = 0; // Обнуляем счетчик правильных ответов
            panelQuiz.SetActive(true); // Включаем панель викторины
            DisplayQuestion(); // Показываем первый вопрос
            isQuizPassed = true; // Ставим флажок того, что викторина пройдена
        }
    }
    // Метод для отображения текущего вопроса
    public virtual void DisplayQuestion()
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

    // Метод для проверки ответа
    public virtual void CheckAnswer(int selectedAnswerIndex)
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
    public virtual void EndQuiz()
    {
        panelQuiz.SetActive(false); // Отключаем панель викторины

        Debug.Log($"Викторина завершена! Правильных ответов: {correctAnswersCount} из {questions.Length}");
    }

    // Метод для запуска второго диалога и выпадения предмета
    public virtual void StartDialogueAndDropItem()
    {
        // Отключаем панель викторины
        panelQuiz.SetActive(false);

        // Запускаем второй диалог через DialogueManager
        if (dialogueManager != null)
        {
            dialogueManager.StartSecondDialogue(); // Предполагается, что DialogueManager содержит метод StartSecondDialogue()
        }

        // Выпадение предмета
        DropItem();
    }

    // Метод для выпадения предмета
    public virtual void DropItem()
    {
        // Позиция, где появится предмет

        Vector3 dropPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        //Создаем проверку, так же проверка не пустая ли ссылка
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, dropPosition, Quaternion.identity);
            Debug.Log("Предмет выпал! Победа");
        }
        else
        {
            Debug.Log("Prefab назначь!");
        }
    }
}
