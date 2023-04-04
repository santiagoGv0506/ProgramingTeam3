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
    public float velocidadDash=8;
    public float tiempoDash=0.4f;
    public float cooldown = 3;
    private float gravedadInicial;
    private bool puedeHacerDash = true;
    private bool puedeMoverse = true;
    private bool DoubleJump;
    private Vector3 respawnPoint;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        gravedadInicial = player.gravityScale;
        respawnPoint = player.position;
    }


    void Update()
    {
        
        if(puedeMoverse)
        {
            player.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, player.velocity.y);
        }
      

        if (Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            if (GroundCheck.isGrounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
            }
            else if (DoubleJump)
            {
                player.velocity = Vector2.zero;
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                DoubleJump = false;
            }
            
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

        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeHacerDash) {
            StartCoroutine(Dash());
        }
        if (GroundCheck.isGrounded) {
            DoubleJump = true;
        }

    }

    private IEnumerator Dash()
    {
        puedeMoverse = false; puedeHacerDash = false; player.gravityScale = 0;  GroundCheck.isGrounded=false;
        if(Input.GetAxis("Horizontal")==0)
        {
            player.velocity = new Vector2(velocidadDash, 0);
        }
        else
        {
            player.velocity = new Vector2(velocidadDash * Input.GetAxis("Horizontal"), 0);
        }
        yield return new WaitForSeconds(tiempoDash);
        puedeMoverse = true; player.gravityScale = gravedadInicial;
        yield return new WaitForSeconds(cooldown);
        puedeHacerDash=true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Instakill")) 
        {
            transform.position = respawnPoint;
        }
        else if (collision.transform.CompareTag("CheckPoint"))
        {
            respawnPoint = transform.position;
        }
    }
}
