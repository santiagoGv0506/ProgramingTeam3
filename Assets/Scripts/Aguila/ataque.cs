using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataque : MonoBehaviour
{
    public float distanceRaycast = 10f;
    public GameObject Aguila;
    public GameObject Picada;

    void Start()
    {
    }


    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down, Color.red, distanceRaycast);
    }

    public void FixedUpdate()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player"))
            {
                //aqui iria la animacion
                Invoke("CaerEnPicada",0.1f);
                Destroy(Aguila,0.1f);
            }
        }
    }

    void CaerEnPicada()
    {
        GameObject newPicada;
        newPicada = Instantiate(Picada,transform.position, transform.rotation);
    }
}
