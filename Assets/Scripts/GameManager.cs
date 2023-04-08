using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    PlayerMovement a;
    public HUD hud;
    public static GameManager instance { get; private set; }
    public GameObject player;
    private int lifes = 3;
    [SerializeField] private float tiempoPerdida;
    private IEnumerator pepe;
    private bool juanReturn;


    private void Awake()
    {
        juanReturn = false;
        pepe = PerderControl();
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        if (juanReturn)
        {
            StopCoroutine(pepe);
            juanReturn = false;
        }
        if (lifes < 1)
        {
            transform.position = PlayerMovement.respawnPoint;
        }
    }

    public void perderVida(Vector2 posicion)
    {
        lifes -= 1;
        player.GetComponent<PlayerMovement>().Bounce(posicion);
        StartCoroutine(pepe);
            if (lifes == 0)
            {
                StartCoroutine("death");
            }
            else
            {
                StartCoroutine("hit");
                hud.Desactivarvidas(lifes);
            }
    }
    public void perderVida()
    {
        lifes -= 1;
        if (lifes == 0)
        {
            StartCoroutine("death");
        }
        else
        {
            StartCoroutine("hit");
            hud.Desactivarvidas(lifes);
        }
    }
    public bool GanarVida()
    {
        if(lifes == 3)
        {
            return false; 
        }
        hud.Activarvidas(lifes);
        lifes += 1;
        return true;
    }
    IEnumerator PerderControl()
    {
        player.GetComponent<PlayerMovement>().puedeMoverse = false;
        yield return new WaitForSeconds(tiempoPerdida);
        player.GetComponent<PlayerMovement>().puedeMoverse = true;
        juanReturn = true;
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
        a = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        a.setMuerto();
        player.GetComponent<Animator>().Play("Death");
        yield return new WaitForSeconds(1f);
        player.GetComponent<Animator>().Play("IDLE");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
