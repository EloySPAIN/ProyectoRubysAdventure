using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    Vector2 position;
    public AudioClip collectedClip;
    public ParticleSystem smokeEffect;
    public AudioClip playerHit;
    AudioSource audioSource;

    public float velocidad;

    public float changeTime = 4.0f;
    float timer;
    int direction = 1;
    public bool invertidoHor;
    public bool invertidoVer;
    public bool unaDireccionHor;
    public bool unaDireccionVer;
    int rotacionMov = 0;
    bool broken = true;
    

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        position = rigidbody2d.position;
        if (unaDireccionHor)
        {
            if (invertidoHor)
            {
                changeRotationHor(!invertidoHor);
            }
            else
            {
                changeRotationHor(!invertidoHor);
            }
        }else if (unaDireccionVer)
        {
            if (invertidoVer)
            {
                changeRotationVer(!invertidoVer);
            }
            else
            {
                changeRotationVer(!invertidoVer);
            }
        }
        else
        {
            switch (rotacionMov)
            {
                case 0:
                    changeRotationHor(invertidoHor);
                    break;
                case 1:
                    position.y = position.y - Time.deltaTime * velocidad; // abajo
                    animator.SetFloat("Move X", 0);
                    animator.SetFloat("Move Y", -direction);
                    break;
                case 2:
                    changeRotationHor(!invertidoHor);
                    break;
                case 3:
                    position.y = position.y + Time.deltaTime * velocidad; // arriba
                    animator.SetFloat("Move X", 0);
                    animator.SetFloat("Move Y", direction);
                    break;
            }
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

            invertidoHor = invertidoHor ? false : true;
            invertidoVer = invertidoVer ? false : true;
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
        smokeEffect.Stop();
        broken = false;
        rigidbody2d.simulated = false;
        animator.SetTrigger("Fixed");
        audioSource.Stop();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            player.PlaySound(playerHit);
        }
    }

    void changeRotationHor(bool derOIz)
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

    void changeRotationVer(bool derOIz)
    {
        if (derOIz)
        {
            position.y = position.y + Time.deltaTime * velocidad; // arriba
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.y = position.y - Time.deltaTime * velocidad; // abajo
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", -direction);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
