using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class erizo : MonoBehaviour
{
    private Vector2 actualPos;
    public Transform[] moveSpots;
    private int i;
    public float speed = 2;
    private float vidaCont;
    private Animator animator;
    private float waitTime;
    private float startWaitTime = 1f;
    public float dir;
    
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        vidaCont = 10;
        waitTime = startWaitTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("player")) 
        {
            GameManager.instance.perderVida();
        }
    }

    private void FixedUpdate()
    {
        actualPos = transform.position;
        StartCoroutine(CheckEnemyMoving());

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
        else if(transform.position.x == actualPos.x)
        {
            if (dir == 1)
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
            }
            else if (dir == -1)
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            }
            animator.Play("idle");
        }
    }

     IEnumerator getDamage(float damage)
    {
        animator.Play("hit");
        yield return new WaitForSeconds(0.1f);
        vidaCont-=damage;
        if (vidaCont<=0)
        {
            StartCoroutine("muerte");
        }
        animator.Play("run");
    }

    IEnumerator muerte()
    {
        animator.Play("death");
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
