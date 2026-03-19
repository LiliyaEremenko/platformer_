using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainChar : Entity
{
    [SerializeField] private int health;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;
    [SerializeField] private Sprite shieldedHeart;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    
    private bool isGrounded;

    public static MainChar Instance { get; set; }

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    public int score;
    public Text scoreText;

    public int shield;

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        lives = 5;
        health = lives;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Instance = this;
        score = 0;
        isRecharged = true;
        isGrounded = true;
    }

    void Update()
    {
        if (isGrounded && !isAttacking) State = States.idle;

        if (!isAttacking && isGrounded && Input.GetButton("Horizontal")) State = States.walk;

        if (Input.GetButtonDown("Fire1"))
            Attack();

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (health > lives)
            health = lives;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < shield)
                hearts[i].sprite = shieldedHeart;
            else if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
        }
    }

    private void FixedUpdate()
    {
        //CheckGround();
    }

//    private void CheckGround()
//    {
//        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
//        isGrounded = collider.Length > 1;
//        if (!isGrounded) State = States.jump;
//    }

    public override void GetDamage()
    {
        if (shield > 0)
            shield--;
        else
            lives--;
            if (lives < 1)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Attack()
    {
        if (isGrounded && isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void Jump()
    {
        State = States.jump;
        isGrounded = false;

        StartCoroutine(JumpAnimation());
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.3f);
        isRecharged = true;
    }

    private IEnumerator JumpAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isGrounded = true;
    }

    public void ScoreUp(int count)
    {
        score += count;

        scoreText.text = score.ToString();
    }

    public void GetShield(int count)
    {
        shield += count;
    }

    public static implicit operator MainChar(MainCharController v)
    {
        throw new NotImplementedException();
    }
}

public enum States
{
    idle,
    walk,
    jump,
    attack
}
