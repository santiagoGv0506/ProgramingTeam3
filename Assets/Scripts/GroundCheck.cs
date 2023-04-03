using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static bool isGrounded;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
    }
}
