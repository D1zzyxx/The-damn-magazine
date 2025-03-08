using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float movingSpeed = 5f;
    private PlayerInputActions playerInputActions; //����������, ������� ���������� ������ �������� ���������

    private Rigidbody2D rb; //������ ���������� ��� ���������� Rigidbody, ������� ��������� ��������� ����������

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; //�������������� ����������� ����
        }

        rb = GetComponent<Rigidbody2D>(); //������������� ��������� Rigidbody2D, ������� ��������� ��������� ����� ���������� ��������
        playerInputActions = new PlayerInputActions(); //������������� Input System, ��� ��������� ��������� �������������  
        playerInputActions.Enable(); //����������� ������� �����(Input System)
    }
    private Vector2 GetMovementVector() // �������, ����������� �������� ������ �������� �� ���������� playerInputActionss
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); 

        return inputVector;
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
