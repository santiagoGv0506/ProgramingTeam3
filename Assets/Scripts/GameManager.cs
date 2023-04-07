using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        if(lifes > 1)
        {
            player.GetComponent<Animator>().Play("Hit");
        }
        if(lifes == 0)
        {
            player.GetComponent<Animator>().Play("Death");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        hud.Desactivarvidas(lifes);
    }
}
