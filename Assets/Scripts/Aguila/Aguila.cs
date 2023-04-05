using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aguila : MonoBehaviour
{
    public float speed = 0.5f;
    public Transform[] moveSpots;
    private int i = 0;
    private Vector2 actualPos;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    void Start()
    {

    }

    void Update()
    {
        StartCoroutine(CheckEnemyMoving()); //para la animacion del personaje

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[i].transform.position, speed*Time.deltaTime);

        if(Vector2.Distance(transform.position, moveSpots[i].transform.position) < 1.0f)
        {
                if (moveSpots[i] != moveSpots[moveSpots.Length - 1])
                {
                    i++;
                }
                else
                {
                    i=0;
                }
        }
    }

    IEnumerator CheckEnemyMoving()
    {
        actualPos = transform.position;

        yield return new WaitForSeconds(0.5f);

        if(transform.position.x > actualPos.x)
        {
            spriteRenderer.flipX = true;
        }
        else if(transform.position.x < actualPos.x)
        {
            spriteRenderer.flipX=false;
        }
    }
}
