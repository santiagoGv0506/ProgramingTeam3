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

    void Start()
    {
        player = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        
        player.velocity = new Vector2(Input.GetAxis("Horizontal")*moveSpeed, player.velocity.y);
      
        if (Input.GetKey("w") && GroundCheck.isGrounded)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
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
    }
}
