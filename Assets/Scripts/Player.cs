using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float movingSpeed = 5f;
    private PlayerInputActions playerInputActions; //����������, ������� ���������� ������ �������� ���������
    

    private Rigidbody2D rb; //������ ���������� ��� ���������� Rigidbody, ������� ��������� ��������� ����������
    private Vector2 movement;

    public Animator animator;
    private SpriteRenderer rbSprite;

    public bool pickUpMagazine = false;
    public bool pickUpBook = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //�������������� ����������� ����
        }

        rb = GetComponent<Rigidbody2D>(); //������������� ��������� Rigidbody2D, ������� ��������� ��������� ����� ���������� ��������
        animator = GetComponent<Animator>();
        rbSprite = GetComponent<SpriteRenderer>();
        playerInputActions = new PlayerInputActions(); //������������� Input System, ��� ��������� ��������� �������������  
        playerInputActions.Enable(); //����������� ������� �����(Input System)
    }
    private Vector2 GetMovementVector() // �������, ����������� �������� ������ �������� �� ���������� playerInputActionss
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 

        return inputVector;
    }
    private void Update()
    {
        if (movement.y > 0) // �������� �����
        {
            animator.SetFloat("moveY", 1); // ���������� �������� ������ �����
        }
        else if (movement.y < 0) // �������� ����
        {
            animator.SetFloat("moveY", -1); // ��������� �������� ������ �����
        }
        else // ��� �������� �� ��� Y
        {
            animator.SetFloat("moveY", 0); // ������������ � ������� ��������
        }
        animator.SetFloat("moveX", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
      
        if (movement.x != 0)
        {
            // ������ ����������� �������
            rbSprite.flipX = (movement.x > 0);
        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = GetMovementVector(); //���������� ��� �������� ����������� �������� ���������

        inputVector = inputVector.normalized; //������ ���, ����� �������� ��������� ��� �������� � ������ ������������ ���� ���������

        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime)); 
        //�����, ������� ��������� ������� ���������, � ������� ������ � ����������� �� ���
        //(�������� �������������� + ����������� �������� * (�������� ���� �������� * ������������� �������� ����� ������� ������))

    }
}
