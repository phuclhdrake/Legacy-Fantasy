using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Animator anim;

    public float jumForce;
    public float speed;
    private Rigidbody2D rb;

    public Transform groundPos;
    private bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool doubleJump;

    //attack 
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    private float nextAttackTime = 0f;
    // health
    public int maxHealth = 100;
    private int currentHealth;
    public ParametersController hpbar;
    //
    public Slider hp;

    // lực đẩy khi bị enemy tấn công
    public float pushForce = 12f;
    private bool isPushed = false;

    // kills
    public int numberKill;
    // bg game over
    public GameObject bgGameOver;
    private GameObject player;

    float moveInput = 0f;
    public Joystick joystick;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        numberKill = 0;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        hpbar.setMaxHpSlider(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (currentHealth <= 0)
        {
            anim.SetBool("isDead", true);

            //rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(0, 0);
            rb.angularVelocity = 0f;
            GetComponent<Collider2D>().enabled = false;
            
            SoundManager.Instance.musicSource.Stop();
            SoundManager.Instance.sfxSource.Stop();
            SoundManager.Instance.PlaySFX("gameOver");
            StartCoroutine(EndGameAfterDelay(1.2f));
        }
        else
        {
            // jumping
            playerJumping();
            // attack
            if (isGrounded == true && Input.GetKeyDown(KeyCode.F))
            {
                if (Time.time >= nextAttackTime)
                {
                    playerAttack();
                    anim.SetTrigger("attack");
                    nextAttackTime = Time.time + 1f / attackRange;
                }
            }
            // runing
            playerRuning();
        }

    }

    public void ButtonAttack()
    {
        if (isGrounded == true && currentHealth > 0)
        {
            if (Time.time >= nextAttackTime)
            {
                playerAttack();
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + 1f / attackRange;
            }
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bot")
        {
            SoundManager.Instance.PlaySFX("PlayerTakeDame");
            anim.SetTrigger("takeDame");
            currentHealth -= 10;
            hp.value = currentHealth;
            // Tính toán hướng đẩy
            Vector2 pushDirection = (transform.position - collision.transform.position).normalized;

            // Áp dụng lực đẩy
            rb.velocity = new Vector2(pushDirection.x * pushForce, rb.velocity.y);

            // Đặt cờ để biết rằng đang bị đẩy
            isPushed = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isPushed && collision.gameObject.CompareTag("bot"))
        {
            isPushed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "End")
        {
            speed = 0;
        }
    }
    //runging
    private void playerRuning()
    {
        float joystickInput = joystick.Horizontal;
        float keyboardInput = Input.GetAxisRaw("Horizontal");

        // Kiểm tra nguồn điều khiển ưa thích (điều kiện ưu tiên joystick)
        if (Mathf.Abs(joystickInput) > 0.1f)
        {
            if (joystick.Horizontal >= .2f)
            {
                moveInput = speed;
            }else if (joystick.Horizontal <= -.2f)
            {
                moveInput = -speed;
            }
            else
            {
                moveInput = 0f;
            }
        }
        else
        {
            moveInput = keyboardInput * speed;
        }

        if (!isPushed)
        {
            rb.velocity = new Vector2(moveInput, rb.velocity.y);

            if (moveInput == 0)
            {
                anim.SetBool("isRuning", false);
            }
            else
            {
                anim.SetBool("isRuning", true);
            }

            if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    //jumping 
    private void playerJumping()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySFX("jump");
            anim.SetTrigger("takeOf");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumForce;
        }
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
            doubleJump = false;
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        if (isGrounded == false && doubleJump == false && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumForce;
        }
    }
    // joystick
    public void ButtonJump()
    {
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);
        if (isGrounded == true)
        {
            SoundManager.Instance.PlaySFX("jump");
            anim.SetTrigger("takeOf");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumForce;
        }
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
            doubleJump = false;
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

            isJumping = false;
        
        if (isGrounded == false && doubleJump == false)
        {
            isJumping = true;
            doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumForce;
        }
    }
    //attack
    void playerAttack()
    {
        SoundManager.Instance.PlaySFX("attack");
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemy)
        {
            Debug.Log("we hit: " + enemy.name);
            enemy.GetComponent<EnemyScript>().TakeDamage(damage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPos == null)
            return;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
    //nhan damage player
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        hpbar.setHpSlider(currentHealth);
    }
    // ham hien game over sau vai giay
    private IEnumerator EndGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Time.timeScale = 0; // dung scence\
        Destroy(gameObject, 0.5f);
        bgGameOver.SetActive(true);
    }
}
