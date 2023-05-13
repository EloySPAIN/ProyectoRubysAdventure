using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 position;

    public float velocidad;
    public bool horizontal;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    public bool derOIz;
    int rotacionMov = 0;
    bool broken = true;
    

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        position = rigidbody2d.position;
        
        switch (rotacionMov)
        {
            case 0:
                changeRotation(derOIz);
                break;
            case 1:          
                position.y = position.y - Time.deltaTime * velocidad; // abajo
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", -direction);
                break;
            case 2:
                changeRotation(!derOIz);
                break;
            case 3:
                position.y = position.y + Time.deltaTime * velocidad; // arriba
                animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
                break;
        }

        if (!broken)
        {
            return;
        }



        rigidbody2d.MovePosition(position);
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = changeTime;
            rotacionMov++;
            rotacionMov = rotacionMov >= 4 ? rotacionMov = 0 : rotacionMov++;
        }
    }

    void FixedUpdate()
    {
        if (!broken)
        {
            return;
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void changeRotation(bool derOIz)
    {
        if (derOIz)
        {
            position.x = position.x + Time.deltaTime * velocidad; // derecha
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        else
        {
            position.x = position.x - Time.deltaTime * velocidad; // izquierda
            animator.SetFloat("Move X", -direction);
            animator.SetFloat("Move Y", 0);
        }
    }
}
