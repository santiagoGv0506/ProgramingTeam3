using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    [SerializeField]private Transform AttackController;
    public float hitRadius;
    public float hitDamage;
    
    void Update()
    {
        if(Input.GetKeyDown("j"))
        {
            Hit();
        }
    }

    private void Hit()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(AttackController.position, hitRadius);
        foreach (Collider2D collider in objects)
        {
            if (collider.CompareTag("Enemy"))
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
}
