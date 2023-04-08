using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool vidaRecuperada =GameManager.instance.GanarVida();
            if (vidaRecuperada)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
