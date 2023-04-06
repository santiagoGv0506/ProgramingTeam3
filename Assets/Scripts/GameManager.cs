using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    public void perderVida()
    {
        lifes -= 1;
        hud.Desactivarvidas(lifes);
    }
}
