using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    PlayerMovement a;
    [SerializeField]private Transform AttackController;
    public float hitRadius;
    public float hitDamage;
    
    void Update()
    {
        if(Input.GetKeyDown("j"))
        {  
            a = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if (a.getMuerto()==false)
            {
                Hit();
                StartCoroutine("hit");
            }
        }
    }

    private void Hit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(AttackController.position, hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy") && collider.isTrigger)
            {
                collider.transform.GetComponent<erizo>().getDamage(hitDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, hitRadius);
    }

    IEnumerator hit()
    {
        GetComponent<Animator>().Play("Attack");
        yield return new WaitForSeconds(0.15f);
        GetComponent<Animator>().Play("IDLE");
    }
}
