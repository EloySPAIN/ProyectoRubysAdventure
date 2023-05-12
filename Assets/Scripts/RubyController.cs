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
    // Start is called before the first frame update
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
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

        //void OnTriggerEnter(Collider other)
        //{
        //    if (other.CompareTag("TriggerObject"))
        //    {
        //        // Desactiva la colisión del primer collider
        //        Collider[] colliders = GetComponents<Collider>();
        //        colliders[1].isTrigger = true;
        //    }
        //}

        //void OnTriggerExit(Collider other)
        //{
        //    if (other.CompareTag("TriggerObject"))
        //    {
        //        // Activa la colisión del primer collider
        //        Collider[] colliders = GetComponents<Collider>();
        //        colliders[1].isTrigger = false;
        //    }
        //}
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}
