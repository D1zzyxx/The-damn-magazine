using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChemistry : MonoBehaviour
{
    public GameObject panel;
    int count = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "TeacherChemistry")&&(count==0))
        {
            panel.SetActive(true);
            count = 1;
        }
    }
}
