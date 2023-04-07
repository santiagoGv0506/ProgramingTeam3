using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerMovement a;
    public HUD hud;
    public static GameManager instance { get; private set; }
    public GameObject player;
    private int lifes = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (lifes < 1)
        {
            transform.position = PlayerMovement.respawnPoint;
        }
    }

    public void perderVida()
    {
        lifes -= 1;
        if(lifes == 0)
        {
            StartCoroutine("death");
        }
        else
        {
            StartCoroutine("hit");
        }
        hud.Desactivarvidas(lifes);
    }

    IEnumerator hit()
    {
        player.GetComponent<Animator>().Play("Hit");
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Animator>().Play("IDLE");
    }

    IEnumerator death()
    {
        a = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        a.setMov();
        a.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Animator>().Play("Death");
        yield return new WaitForSeconds(1f);
        player.GetComponent<Animator>().Play("IDLE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
