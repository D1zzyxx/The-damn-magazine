using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScreamer : MonoBehaviour
{
    public GameObject scaryImage;

   
    private IEnumerator ShowScreamer()
    {
        scaryImage.SetActive(true);
        yield return new WaitForSeconds(1f); // Длительность показа
        scaryImage.SetActive(false);
        gameObject.SetActive(false); // Отключаем ловушку, чтобы сработала один раз
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(ShowScreamer());
        }
    }

}
