using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    // Название сцены, куда будет переход
    public string targetSceneName;
    // Позиция, куда попадет игрок в новой сцене
    public Vector3 spawnPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {       
        // Проверяем, что объект имеет нужный тег и сталкивается с игроком
        if (other.CompareTag("Player") && gameObject.CompareTag("Door"))
        {
            // Сохраняем позицию игрока в GameManager
            GameSaver.Instance.playerData.position = spawnPosition;
                     
            Debug.Log("Загружаем сцену");            
            
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
