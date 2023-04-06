using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine("desapear");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator desapear()
    {
        yield return new WaitForSeconds(2.17f);
        Destroy(gameObject);
    }
}
