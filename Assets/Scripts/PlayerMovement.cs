using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D player;

    public float moveSpeed = 2;
    public float jumpSpeed = 3;
    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 1f;
    public float velocidadDash=12;
    public float tiempoDash=1;
    private float gravedadInicial;
    private bool puedeHacerDash = true;
    private bool puedeMoverse = true;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        gravedadInicial = player.gravityScale;
    }


    void Update()
    {
        
        if(puedeMoverse)
        {
            player.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, player.velocity.y);
        }
      

        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space")) && GroundCheck.isGrounded)
        {
            player.velocity = new Vector2(player.velocity.x,jumpSpeed);
        }

        if (betterJump)
        {
            if (player.velocity.y < 0)
            {
                player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (player.velocity.y > 0 && !Input.GetKey("w"))
            {
                player.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;

            }
        }

        if (Input.GetAxis("Fire1")>0 && puedeHacerDash) {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        puedeMoverse = false; puedeHacerDash = false; player.gravityScale = 0;
        if(Input.GetAxis("Horizontal")==0)
        {
            player.velocity = new Vector2(velocidadDash, 0);
        }
        else
        {
            player.velocity = new Vector2(velocidadDash * Input.GetAxis("Horizontal"), 0);
        }
        yield return new WaitForSeconds(tiempoDash);
        puedeMoverse = true; puedeHacerDash = true; player.gravityScale = gravedadInicial;
    }
}
