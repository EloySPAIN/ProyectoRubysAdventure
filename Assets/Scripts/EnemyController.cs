using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocidad;

    Rigidbody2D rigidbody2d;
    public bool horizontal;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    Vector2 position;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    void Update()
    {
        position = rigidbody2d.position;
        if (horizontal)
        {
            position.x = position.x + Time.deltaTime * velocidad;
        }
        else
        {
            position.x = position.x - Time.deltaTime * velocidad;

        }

        rigidbody2d.MovePosition(position);
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;

            horizontal = horizontal ? false : true;

        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    RubyController player = other.gameObject.GetComponent<RubyController>();

    //    if (player != null)
    //    {
    //        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        player.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    //        player.ChangeHealth(-1);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
