using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject[] lifes;
  

    void Update()
    {
        
    }
    public void Desactivarvidas(int index)
    {
        lifes[index].gameObject.SetActive(false);
    }

    public void Activarvidas(int index)
    {
        lifes[index].gameObject.SetActive(true);
    }

}
