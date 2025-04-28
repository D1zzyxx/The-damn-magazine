using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhysicsDialogue : MonoBehaviour
{
    /* СУКА БЛЯТЬ НАХУЙ ВСЕ ЗАЕБАЛО
     * Тут я поменял местами массивы, а именно в массиве препода речь Зины
     * и в массиве Зины речь препода, я не хочу все переписывать из-за одного квеста
     * Делать мне нехуй, работать будет и не ебет
     */

    public GameObject player; // Ссылка на игрока, далее у него будем ВКЛ/ВЫКЛ Ходьбу
    private Player playerMove;


    public TextMeshProUGUI line; // Ссылка на TMPUGI для вывода строки
    public Button buttonNext; // Ссылка на кнопку "Далее"
    public GameObject panelDialog; // Ссылка на панель диалога

    private int indexLine; // Индекс текущей строки диалога
    private bool isOrder; // Флажок очередности (преподаватель/игрок)
    private bool isSecondDialogue = false; // Флаг для второго диалога
    private bool isDialogueLost; // Флаг о том проигрывался ли диалог

    private string[] dialogTeacherFirst = 
    {   "Зинаида: Здравствуйте! Скажите, пожалуйста, у вас нет ли случайно журнала моей группы?",
        "Зинаида уже сидит за партой 10 минут." + "Единственное, что она может услышать…",
        "Думаю, бесполезно ждать. Я буду просто молиться, чтобы журнал был не здесь." + "Простите, можно выйти", 
    };

    private string[] dialogPlayerFirst =
    {
        "Виктория Арсентьева: Подожди, я сейчас занят. Дай мне 5 минут.",
        "Бла-бла-бла-бла-бла-бла-бла",
        "Виктория Арсентьева: Ой, совсем не хочешь учиться, да? Ладно, выходи, но только возвращайся быстрее!",
    };

    private void Start()
    {
        isDialogueLost = false;
        panelDialog.SetActive(false); // Отключаем диалоговую панель при старте
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        isSecondDialogue = false; // Начинаем с первого диалога

        playerMove = player.GetComponent<Player>(); // Получаем скрипт для дальнейших манипуляций
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

    // Метод для запуска первого диалога
    public void StartFirstDialogue()
    {
        isSecondDialogue = false; // Устанавливаем флаг для первого диалога
        indexLine = 0; // Обнуляем индекс
        isOrder = true; // Первым говорит преподаватель
        panelDialog.SetActive(true); // Включаем панель диалога
        ActivateDialogue(); // Показываем первую строку диалога

        playerMove = player.GetComponent<Player>(); // Получаем скрипт для дальнейших манипуляций
        playerMove.enabled = false; //Отключаем ходьбу

    }

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
    }
    private void EndFirstDialogue()
    {
        panelDialog.SetActive(false); // Выключаем панель диалога
        buttonNext.interactable = false; // Выключаем кнопку "Далее"
        Debug.Log("Диалог завершен");
        playerMove.enabled = true;
    }
}
