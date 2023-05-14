using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }
    public float velocidad = 5f;

    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;

    bool isInvincible;
    float invincibleTimer;
    float horizontal;
    float vertical;


    Rigidbody2D rigidbody2d;
    Vector2 position;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);


    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
        currentHealth = maxHealth;
    }


    void Update()
    {
        position = rigidbody2d.position;
        if (Input.GetKey(KeyCode.D))
        {
            position.x = position.x + velocidad * Time.deltaTime;
            horizontal = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x = position.x - velocidad * Time.deltaTime;
            horizontal = -1f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y = position.y + velocidad * Time.deltaTime;
            vertical = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y = position.y - velocidad * Time.deltaTime;
            vertical = -1f;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }


        rigidbody2d.MovePosition(position);

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        horizontal = 0;
        vertical = 0;
        
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Proyectil projectile = projectileObject.GetComponent<Proyectil>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
