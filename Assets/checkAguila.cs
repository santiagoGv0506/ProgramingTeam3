using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAguila : MonoBehaviour
{
    public static bool groundAguila;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("suelo"))
        {
            groundAguila = true;
        }
        else
        {
            groundAguila = false;
        }
    }
}
