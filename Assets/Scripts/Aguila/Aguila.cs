using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Aguila : MonoBehaviour
{
    public GameObject aguila;
    private float distanceRaycast = 10f;
    public float speed = 5f;
    public Transform[] moveSpots;
    private int i = 0;
    private Vector2 actualPos;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool yaRoto;
    private float dir = 1;
    private Vector2 largo;
    private Vector2 medio;
    private Vector2 corto;
    private Vector2 temp;
    public static bool isGrounded;
    private IEnumerator movim;
    private bool golpe;
    private BoxCollider2D boxCollider;
  

    void Start()
    {
        yaRoto = false;
        golpe = false;
        largo = new Vector2(8.66f, -5f);
        medio = new Vector2(7.07f, -7.07f);
        corto = new Vector2(1.74f, -9.85f);
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        actualPos = transform.position;

        StartCoroutine(CheckEnemyMoving()); //para la animacion del personaje

        Debug.DrawRay(actualPos + new Vector2(0.25f * dir, 0), new Vector2(largo.x * dir, largo.y) , Color.red);
        Debug.DrawRay(actualPos + new Vector2(0.25f * dir, 0), new Vector2(medio.x * dir, medio.y) , Color.green);
        Debug.DrawRay(actualPos + new Vector2(0.25f * dir, 0), new Vector2(corto.x * dir, corto.y) , Color.blue);

        RaycastHit2D largoR = Physics2D.Raycast(actualPos + new Vector2(0.25f * dir, 0), new Vector2(largo.x * dir, largo.y), distanceRaycast);
        RaycastHit2D medioR = Physics2D.Raycast(actualPos + new Vector2(0.25f * dir, 0), new Vector2(medio.x * dir, medio.y), distanceRaycast);
        RaycastHit2D cortoR = Physics2D.Raycast(actualPos + new Vector2(0.25f * dir, 0), new Vector2(corto.x * dir, corto.y), distanceRaycast);
        if (largoR.collider != null && largoR.collider.CompareTag("Player"))
        {
            if (yaRoto==false)
            {
                temp = new Vector3(8.66f * dir, -5,0);
                rotar(largoR.collider);
            }
        }
        else if(medioR.collider != null && medioR.collider.CompareTag("Player"))
        {
            if (yaRoto == false)
            {
                temp = new Vector3(7.07f * dir, -7.07f,0);
                rotar(medioR.collider);
            }
        }
        else if(cortoR.collider != null && cortoR.collider.CompareTag("Player"))
        {
            if (yaRoto == false)
            {
                temp = new Vector3(1.74f * dir, -9.85f,0);
                rotar(cortoR.collider);
            }
        }


            if (yaRoto==false)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 1.0f)
            {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
        }


        if (checkAguila.groundAguila)
        {
            StopCoroutine(movim);
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine("muerte");
        }

    }

    IEnumerator CheckEnemyMoving()
    {
        yield return new WaitForSeconds(0.2f);

        if (transform.position.x > actualPos.x)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            dir = 1;
        }
        else if (transform.position.x < actualPos.x)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            dir = -1;
        }
    }

    void rotar(Collider2D hit)
    {
        yaRoto = true;
        //transform.right = hit.transform.position-transform.position;
        movim = mov(hit.transform.position);
        StartCoroutine(movim);
    }

    IEnumerator mov(Vector2 target)
    {
        target = temp + target;
        animator.Play("atack");
        yield return new WaitForSeconds(0.55f);

        while (actualPos != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.transform.CompareTag("Player"))
        {
            StopCoroutine(movim);
            GameManager.instance.perderVida();
            Destroy(aguila);
        }
    }

    IEnumerator muerte()
    {
        animator.Play("muerte");
        yield return new WaitForSeconds(2.17f);
        Destroy(aguila);
    }
}
