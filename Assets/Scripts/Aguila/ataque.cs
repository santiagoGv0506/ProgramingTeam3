using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ataque : MonoBehaviour
{
    public float distanceRaycast = 10f;

    public GameObject Picada;

    public float cooldownAttack=3f;
    private float actualCooldownAttack;

    void Start()
    {
        actualCooldownAttack = 0;
    }


    void Update()
    {
        actualCooldownAttack -= Time.deltaTime;
        Debug.DrawRay(transform.position, Vector2.down, Color.red, distanceRaycast);
    }

    public void FixedUpdate()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, Vector2.down, distanceRaycast);

        if (hit2D.collider != null)
        {
            if (hit2D.collider.CompareTag("Player") && actualCooldownAttack<0)
            {
                Invoke("CaerEnPicada", 0.5f);
                //aqui iria la animacion
                actualCooldownAttack = cooldownAttack;
            }
        }
    }

    void CaerEnPicada()
    {
        GameObject newPicada;
        newPicada = Instantiate(Picada,transform.position, transform.rotation);
    }
}
