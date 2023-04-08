using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Aguila : MonoBehaviour
{
    public GameObject aguila;
    private float distanceRaycast = 30f;
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
    private IEnumerator muerto;
    private bool colliderSapoHP;


    void Start()
    {
        muerto = muerte();
        yaRoto = false;
        largo = new Vector2(8.66f, -5f);
        medio = new Vector2(7.07f, -7.07f);
        corto = new Vector2(1.74f, -9.85f);
        colliderSapoHP = true;
    }


    void FixedUpdate()
    {
        actualPos = transform.position;

        StartCoroutine(CheckEnemyMoving());

        if (yaRoto == false)
        {
            Debug.DrawRay(actualPos + new Vector2(0.30f * dir, 0), new Vector2(largo.x * dir, largo.y), Color.red);
            Debug.DrawRay(actualPos + new Vector2(0.30f * dir, 0), new Vector2(medio.x * dir, medio.y), Color.green);
            Debug.DrawRay(actualPos + new Vector2(0.30f * dir, 0), new Vector2(corto.x * dir, corto.y), Color.blue);

            RaycastHit2D[] largoR = Physics2D.RaycastAll(actualPos + new Vector2(0.30f * dir, 0), new Vector2(largo.x * dir, largo.y), distanceRaycast);
            RaycastHit2D[] medioR = Physics2D.RaycastAll(actualPos + new Vector2(0.30f * dir, 0), new Vector2(medio.x * dir, medio.y), distanceRaycast);
            RaycastHit2D[] cortoR = Physics2D.RaycastAll(actualPos + new Vector2(0.30f * dir, 0), new Vector2(corto.x * dir, corto.y), distanceRaycast);

            foreach (RaycastHit2D hit2D in largoR)
            {
                if (hit2D.collider != null && hit2D.collider.CompareTag("Player") && yaRoto == false)
                {
                    temp = new Vector3(8.66f * dir, -5, 0);
                    rotar(hit2D.collider);
                }
            }
            foreach (RaycastHit2D hit2D in medioR)
            {
                if (hit2D.collider != null && hit2D.collider.CompareTag("Player") && yaRoto == false)
                {
                    temp = new Vector3(7.07f * dir, -7.07f, 0);
                    rotar(hit2D.collider);
                }
            }
            foreach (RaycastHit2D hit2D in cortoR)
            {
                if (hit2D.collider != null && hit2D.collider.CompareTag("Player") && yaRoto == false && yaRoto == false)
                {
                    temp = new Vector3(1.74f * dir, -9.85f, 0);
                    rotar(hit2D.collider);
                }
            }

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
            colliderSapoHP = false;
            StopCoroutine(movim);
            StartCoroutine(muerto);
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
        transform.right = hit.transform.position - transform.position;
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
        if (collision.transform.CompareTag("Player") && colliderSapoHP)
        {
            StopCoroutine(movim);
            GameManager.instance.perderVida();
            Destroy(aguila);
        }
    }

    IEnumerator muerte()
    {
        checkAguila.groundAguila = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        animator.Play("muerte");
        yield return new WaitForSeconds(2.17f);
        Destroy(aguila);
    }
}
