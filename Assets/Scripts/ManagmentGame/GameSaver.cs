using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public static GameSaver instanse;

    public PlayerData playerData;
   

    void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Player.Instance.pickUpBook = Convert.ToBoolean(PlayerPrefs.GetInt("BookUp"));
        Player.Instance.pickUpMagazine = Convert.ToBoolean(PlayerPrefs.GetInt("MagazineUp"));
    }
    private void Update()
    {
        if(Player.Instance.pickUpBook)
        {
            PlayerPrefs.SetInt("BookUp", 1);
            PlayerPrefs.Save();
        }
        if(Player.Instance.pickUpMagazine)
        {
            PlayerPrefs.SetInt("MagazineUp", 1);
            PlayerPrefs.Save();
        }

    }

}

[System.Serializable]
public class PlayerData
{
    public Vector3 position;
}
      



