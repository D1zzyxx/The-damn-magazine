using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private Transform player; // �������� ������ �����
    public float smoothSpeed = 0.12f; // ����������� ������������ ������

    public float minX, minY, maxX, maxY; // ������������ ������, ����� �� ������� ���� �� ����

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // �������� ������� ������ �� ����
    }

    private void LateUpdate() // LateUpdate ��� ����������� ����������
    {
        Vector3 moveCamera = transform.position; // �������� ������� ������, ��� ����������� ���������

        moveCamera.x = player.position.x; 
        moveCamera.y = player.position.y; // ������ ������ ������� ������

        Vector3 smoothMove = Vector3.Lerp(transform.position, moveCamera, smoothSpeed); // ����� ���� ������ ������� ����������� ������

        // ������������ ������� ������, ����� ��� �� �������� �� �����
        smoothMove.x = Mathf.Clamp(smoothMove.x, minX, maxX);
        smoothMove.y = Mathf.Clamp(smoothMove.y, minY, maxY);

        transform.position = smoothMove; // ������������� ������� ������
    }
}
