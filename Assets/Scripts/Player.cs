using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float movingSpeed = 5f;
    private PlayerInputActions playerInputActions; //Переменная, которая подключает скрипт движения персонажа

    private Rigidbody2D rb; //Создаю переменную для компонента Rigidbody, которая позволяет управлять персонажем
    private Vector2 movement;

    public Animator animator;
    private SpriteRenderer rbSprite;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //инициализируем статическое поле
        }

        rb = GetComponent<Rigidbody2D>(); //Инициализирую компонент Rigidbody2D, которая позволяет персонажу иметь физические свойства
        animator = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        playerInputActions = new PlayerInputActions(); //Инициализирую Input System, она позволяет персонажу передвигаться  
        playerInputActions.Enable(); //Подключение системы ввода(Input System)
    }
    private Vector2 GetMovementVector() // Функция, позволяющая получить вектор движения из переменной playerInputActionss
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 

        return inputVector;
    }
    private void Update()
    {
        animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0)
        {
            // Меняем направление спрайта
            rbSprite.flipX = (movement.x > 0);
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = GetMovementVector(); //Переменная для хранения направления движения персонажа

        inputVector = inputVector.normalized; //Делает так, чтобы скорость персонажа при движении в разных направлениях была одинакова

        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime)); 
        //Метод, который принимает позицию персонажа, и двигает объект в зависимости от его
        //(текущего местоположения + направление движения * (заданную нами скорость * настраиваемый интервал смены частоты кадров))
    }
}
