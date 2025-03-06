using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScene : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("+2English");
    }    

    public void ExitGame()
    {
        Application.Quit();
    }

    
}
