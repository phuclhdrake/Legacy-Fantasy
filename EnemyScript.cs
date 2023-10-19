using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float start, end;
    private bool isRight;
    public float speed;
    private int WALK = 0;
    private float speedEnemy;
    public GameObject player;
    public Animator anim;
    private Rigidbody2D rbEnemy;
    // thanh mau
    public int health;
    [SerializeField] EnemyHpBar enemyHpBar;
    //
    private float timeChoang;
    public float startTimeChoang;
    private float timeAttack;
    public float startTimeAttack;
    private bool isup = true;
    private bool isAttack = false;
    private float pushDistance = 6f;
    private void Awake()
    {
        enemyHpBar = GetComponentInChildren<EnemyHpBar>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speedEnemy = speed;
        rbEnemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        //choangs
        setTimeDizzy();
        //attack
        EnemyAttack();

        //di chuyen {
        if (isup)
        {
            var positionEnemyX = transform.position.x;
            var positionEnemyY = transform.position.y;
            // di theo player
            if (player != null)
            {
                var positionplayerX = player.transform.position.x;
                var positionplayerY = player.transform.position.y;

                if (positionplayerX > start && positionplayerX < end)
                {
                    // Kiểm tra vị trí Y của người chơi so với kẻ địch
                    float verticalThreshold = 1.0f;
                    if (positionplayerY > positionEnemyY + verticalThreshold || positionplayerY < positionEnemyY - verticalThreshold)
                    {
                        // player ở trên hoặc dưới kẻ thù, ngừng theo dõi
                    }
                    else
                    {
                        // player đang ở ngưỡng Y, tiếp tục theo dõi
                        if (positionplayerX < positionEnemyX)
                        {
                            isRight = false;
                        }
                        if (positionplayerX > positionEnemyX)
                        {
                            isRight = true;
                        }
                    }
                }
            }

            if (positionEnemyX < start)
            {
                isRight = true;
            }
            if (positionEnemyX > end)
            {
                isRight = false;
            }
            Vector2 scale = transform.localScale;
            if (isRight)
            {
                scale.x = -1;
                transform.Translate(Vector2.right * speedEnemy * Time.deltaTime);
                anim.SetInteger("status", WALK);
            }
            else
            {
                scale.x = 1;
                transform.Translate(Vector2.left * speedEnemy * Time.deltaTime);
                anim.SetInteger("status", WALK);
            }
            transform.localScale = scale;
        }
        // } di chuyen
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            isAttack = true;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "thanIdle")
        {
            isRight = isRight ? false : true;
            //isRight = !isRight;
        }
    }

    //damage
    public void TakeDamage(int damage)
    {
        timeChoang = startTimeChoang;
        anim.SetTrigger("hit");
        SoundManager.Instance.PlaySFX("dameBot");
        health -= damage;
        enemyHpBar.UpdateHpBar(health, damage);
        //die
        if (health <= 0)
        {
            dieEnemy();

        }
        Debug.Log("Damage Take ");
    }
    void dieEnemy()
    {
        Debug.Log("enemy die");
        // anim
        anim.SetBool("isDie", true);
        //sound
        SoundManager.Instance.PlaySFX("botDie");
        //die enemy
        rbEnemy.bodyType = RigidbodyType2D.Kinematic;
        rbEnemy.velocity = Vector2.zero;
        rbEnemy.angularVelocity = 0f;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 3f);
        PlayerController.Instance.numberKill++ ; 
        UiController.intance.txtKill.text = PlayerController.Instance.numberKill.ToString() ; 
    }

    void setTimeDizzy()
    {
        if (timeChoang <= 0)
        {
            speedEnemy = speed;
        }
        else
        {
            speedEnemy = 0;
            timeChoang -= Time.deltaTime;
            var scaleEnemy = transform.localScale;
            if (scaleEnemy.x == 1)
            {
                // Enemy đang hướng về bên phải, nên đẩy về bên trái
                transform.Translate(Vector2.right * pushDistance * Time.deltaTime);
            }
            else if (scaleEnemy.x == -1)
            {
                // Enemy đang hướng về bên trái, nên đẩy về bên phải
                transform.Translate(Vector2.left * pushDistance * Time.deltaTime);
            }
        }
    }
    void EnemyAttack()
    {

        if (timeAttack <= 0)
        {
            speedEnemy = speed;
        }
        else
        {
            speedEnemy = 0;
            timeAttack -= Time.deltaTime;
        }
        if (isAttack)
        {
            // Nếu kẻ địch đang tấn công và người chơi tồn tại
            if (player != null)
            {
                timeAttack = startTimeAttack;
                //anim.SetInteger("status", ATTACK);
                anim.SetTrigger("attack");
                isAttack = false;
            }
        }
    }


}
