using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;
    public int health { get { return currentHealth; } }
    public float velocidad = 5f;
    Rigidbody2D rigidbody2d;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        currentHealth = maxHealth;
    }


    void Update() {
        Vector2 position = rigidbody2d.position;
        if (Input.GetKey(KeyCode.D))
        {
            position.x = position.x + velocidad * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
        if (Input.GetKey(KeyCode.A))
        {
            position.x = position.x - velocidad * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

        if (Input.GetKey(KeyCode.W))
        {
            position.y = position.y + velocidad * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }
        if (Input.GetKey(KeyCode.S))
        {
            position.y = position.y - velocidad * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        }

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
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}
