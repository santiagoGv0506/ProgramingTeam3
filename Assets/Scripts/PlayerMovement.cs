using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool muerto;
    public static Vector3 respawnPoint;
    Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        gravedadInicial = player.gravityScale;
        respawnPoint = player.position;
        muerto = false;
        animator = player.GetComponent<Animator>();
        sprite = player.GetComponent<SpriteRenderer>();
        if (GameInfo.reachedCheckpoint)
        {
         Loadgame();
        }
    }


    void Update()
    {
        
        if(puedeMoverse)
        {
            player.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, player.velocity.y);
            if (Input.GetAxis("Horizontal") ==-1||Input.GetKeyDown("a"))
            {
                transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
                animator.SetBool("Runing", true);

            }
            else if (Input.GetAxis("Horizontal") == 1 || Input.GetKeyDown("d"))
            {
                transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                animator.SetBool("Runing", true);
            }
            else
            {
                animator.SetBool("Runing", false);
            }
        }


        if (GroundCheck.isGrounded == false)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Runing", false);
        }
        if (Input.GetKeyDown("w"))
        {

            if (!muerto)
            {
                if (GroundCheck.isGrounded)
                {
                    animator.SetBool("Jump", true);
                    player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                }
                else if (DoubleJump)
                {
                    player.velocity = Vector2.zero;
                    player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                    DoubleJump = false;
                }
            }
            
        }

        if (betterJump && !muerto)
        {
            if (player.velocity.y < 0)
            {
                player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (player.velocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                player.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;

            }

        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && puedeHacerDash) {
            StartCoroutine(Dash());
        }
        if (GroundCheck.isGrounded) {
            DoubleJump = true;
            animator.SetBool("Jump", false);
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.transform.CompareTag("CheckPoint"))
        {

            Safegame();
        }
    }
    private void Safegame()
    {
        GameInfo.position = transform.position;
        GameInfo.reachedCheckpoint = true;
    }
    private void Loadgame()
    {
        transform.position = GameInfo.position;
    }
    
    public void setMov()
    {
        puedeMoverse=false;
    }

    public void setMuerto()
    {
        muerto=true;
    }

    public bool getMuerto()
    {
        return muerto;
    }
}
