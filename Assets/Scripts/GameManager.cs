using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HUD hud;
    public static GameManager instance { get; private set; }

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
            SceneManager.LoadScene(0);
        }
        hud.Desactivarvidas(lifes);
    }
}
