using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picada : MonoBehaviour
{
    public float speed = 2;
    public GameObject Muerte;
    public GameObject spawn;


    private void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.instance.perderVida();
        }
 
        if (collision.transform.CompareTag("suelo"))
        {
            GameObject newTonto;
            newTonto = Instantiate(Muerte, spawn.transform.position, transform.rotation);
            Destroy(gameObject);
        }

        
    }
}
