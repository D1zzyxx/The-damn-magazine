using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    // �������� �����, ���� ����� �������
    public string targetSceneName;
    // �������, ���� ������� ����� � ����� �����
    public Vector3 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {       
        // ���������, ��� ������ ����� ������ ��� � ������������ � �������
        if (other.CompareTag("Player") && gameObject.CompareTag("Door"))
        {
            // ��������� ������� ������ � GameManager
            GameSaver.Instance.playerData.position = spawnPosition;
                     
            Debug.Log("��������� �����");            
            
            SceneManager.LoadScene(targetSceneName);
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene("+2English");
    }    

    public void ExitGame()
    {
        Application.Quit();
    }

   


}
