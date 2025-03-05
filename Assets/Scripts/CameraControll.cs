using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private Transform player; // получаем оюъект перса
    public float smoothSpeed = 0.12f; // сглаживание передвижение камеры

    public float minX, minY, maxX, maxY; // ограничители камеры, чтобы не улетала куда не надо

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // получаем позицию игрока по тегу
    }

    private void LateUpdate() // LateUpdate дл€ корректного обновлени€
    {
        Vector3 moveCamera = transform.position; // получаем позицию камеры, дл€ дальнейшего изменени€

        moveCamera.x = player.position.x; 
        moveCamera.y = player.position.y; // задаем камере позицию игрока

        Vector3 smoothMove = Vector3.Lerp(transform.position, moveCamera, smoothSpeed); // через лерп задаем плавное перемещение камеры

        // ќграничиваем позицию камеры, чтобы она не выходила за рамки
        smoothMove.x = Mathf.Clamp(smoothMove.x, minX, maxX);
        smoothMove.y = Mathf.Clamp(smoothMove.y, minY, maxY);

        transform.position = smoothMove; // устанавливаем позицию камере
    }
}
