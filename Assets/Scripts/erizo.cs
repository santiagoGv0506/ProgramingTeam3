using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erizo : MonoBehaviour
{
    private Vector2 actualPos;
    public Transform[] moveSpots;
    private int i;
    public float speed = 2;
    public float vidaCont = 10;
    private Animator animator;
    private float waitTime;
    private float startWaitTime = 1f;
    private float dir;
    private bool puedeMoverse;
    private bool muerto;


    void Start()
    {
        animator = GetComponent<Animator>();
        puedeMoverse = true;
        waitTime = startWaitTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.perderVida();
        }
    }

    private void FixedUpdate()
    {
        actualPos = transform.position;
        StartCoroutine(CheckEnemyMoving());

        if (puedeMoverse)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpots[i].transform.position) < 1.0f)
            {
                if (waitTime <= 0)
                {
                    if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
    }

    IEnumerator CheckEnemyMoving()
    {
        yield return new WaitForSeconds(0.2f);

        if (transform.position.x > actualPos.x)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            animator.Play("run");
            dir = 1;
        }
        else if (transform.position.x < actualPos.x)
        {
            transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            animator.Play("run");
            dir = -1;
        }
        else if (transform.position.x == actualPos.x)
        {
            if (dir == 1)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            else if (dir == -1)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            if (puedeMoverse)
            {
                animator.Play("idle");
            }
        }
    }

    public void getDamage(float damage)
    {
        StartCoroutine("hit");

        vidaCont -= damage;
        if (vidaCont <= 0)
        {
            muerto = true;
        }
    }


    IEnumerator hit()
    {
        if (muerto)
        {
            puedeMoverse = false;
            animator.SetBool("muerto", true);
            animator.Play("death");
            yield return new WaitForSeconds(1.29f);
            Destroy(gameObject);
        }
        else
        {
            puedeMoverse = false;
            if (dir == 1)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            else if (dir == -1)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            
            animator.Play("hit");
            yield return new WaitForSeconds(0.5f);
            puedeMoverse = true;
            animator.Play("idle");
        }
    }
}
