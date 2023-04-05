using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muerte : MonoBehaviour
{
    public GameObject Muerte;

    public void FixedUpdate()
    {
        GameObject newTonto;
        newTonto = Instantiate(Muerte, transform.position, transform.rotation);
    }
}
