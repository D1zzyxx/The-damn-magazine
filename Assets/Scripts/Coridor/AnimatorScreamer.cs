using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScreamer : MonoBehaviour
{
    public GameObject scaryImage;

   
    private IEnumerator ShowScreamer()
    {
        scaryImage.SetActive(true);
        yield return new WaitForSeconds(1f); // ������������ ������
        scaryImage.SetActive(false);
        gameObject.SetActive(false); // ��������� �������, ����� ��������� ���� ���
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            StartCoroutine(ShowScreamer());
        }
    }

}
