/*
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;

public class BossYokaiAI : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;
    public float distance;
    private Transform target;
    public float followSpeed;
    private float originalFollowSpeed;
    public LayerMask playerLayer;
    public Transform attackPoint;
    private float attackRange = 2f;

    private Animator anim;
    public float damageToPlayer = 10;
    Vector3 demonPos;
    public float damage = 15f;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        originalFollowSpeed = followSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }


    void Update()
    {
        bossAi();
        demonPos = transform.position - new Vector3(0, 2f, 0);

    }
    void bossMove()
    {

        // Oyuncuyu takip etmesi

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        FlipSpriteBasedOnDirection();


    }
    private void bossAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetBool("Attack", true);

            BossFollow();
            StartCoroutine(StopForAttack());

        }
        else
        {

            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetBool("Attack", false);
            bossMove();
        }
    }

    void BossFollow()
    {


        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
    }
    private void FlipSpriteBasedOnDirection()
    {


        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
    }
    IEnumerator StopForAttack()
    {

        followSpeed = 0;
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(1f);


        RaycastHit2D hitEnemy = Physics2D.Raycast(demonPos, -transform.right, distance, playerLayer);
        if (hitEnemy.collider != null)
        {

            StartCoroutine(StopForAttack());
            //AttackOnAnimation();
        }
        else
        {

            anim.SetBool("Attack", false);
            anim.SetBool("Walking", true);
            followSpeed = originalFollowSpeed;
        }



    }

    void AttackOnAnimation()
    {


        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);


        foreach (var player in hitPlayer)
        {
            if (player.GetComponent<CristalCharControler>() != null) { player.GetComponent<CristalCharControler>().takeDamage(damage); }
            if (player.GetComponent<Metal_Char_Combat>() != null) { player.GetComponent<Metal_Char_Combat>().takeDamage(damage); }
        }
    }

}



*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossYokaiAI : MonoBehaviour
{
    public float leftrightspeed;
    public float distance;
    public float followSpeed;
    public Transform attackPoint;
    public float attackRange = 2f;
    public float midAttackRange = 4f;
    public LayerMask playerLayer;

    public float damageNormal = 10f;
    public float damageMid = 20f;
    public float damageSpecial = 40f;

    private float currentHealth;
    public float maxHealth = 200f;

    private float oldPosition;
    private float originalFollowSpeed;
    private Transform target;
    private Animator anim;
    private Rigidbody2D rb;

    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        oldPosition = transform.position.x;
        originalFollowSpeed = followSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (isDead) return;

        if (!isAttacking)
        {
            BossAI();
            FlipSpriteBasedOnDirection();
        }
    }

    void BossAI()
    {
        float playerDistance = Vector2.Distance(transform.position, target.position);

        if (playerDistance <= attackRange)
        {
            StartCoroutine(Attack("Attack1", damageNormal));
        }
        else if (playerDistance <= midAttackRange)
        {
            StartCoroutine(Attack("Attack2", damageMid));
        }
        else if (currentHealth <= maxHealth / 2f && playerDistance <= midAttackRange + 1.5f)
        {
            StartCoroutine(Attack("Attack3", damageSpecial));
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followSpeed * Time.deltaTime);
        anim.SetBool("Walking", true);
    }

    IEnumerator Attack(string animationTrigger, float damage)
    {
        isAttacking = true;
        followSpeed = 0;
        anim.SetBool("Walking", false);
        anim.SetTrigger(animationTrigger);

        yield return new WaitForSeconds(0.6f); // saldýrý animasyon zamanlamasý

        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (var player in hitPlayers)
        {
            if (player.GetComponent<CristalCharControler>() != null)
                player.GetComponent<CristalCharControler>().takeDamage(damage);

            if (player.GetComponent<Metal_Char_Combat>() != null)
                player.GetComponent<Metal_Char_Combat>().takeDamage(damage);
        }

        yield return new WaitForSeconds(0.5f);
        followSpeed = originalFollowSpeed;
        isAttacking = false;
    }

    void FlipSpriteBasedOnDirection()
    {
        if (transform.position.x > oldPosition)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        else if (transform.position.x < oldPosition)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        oldPosition = transform.position.x;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        anim.SetBool("Walking", false);
        anim.SetTrigger("Hurt");
        anim.SetBool("Walking", true);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2f);
    }

}
