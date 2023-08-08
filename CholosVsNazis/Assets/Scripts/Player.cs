using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float Horizontal;
    private bool Grounded;
    private int Health = 3;
    private bool isInvulnerable = false; // Variable de control de invulnerabilidad
    public float invulnerableDuration = 1.0f; // Duración de la invulnerabilidad en segundos

    public GameObject BulletPrefab;
    public float Jumpforce;
    public float Speed;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("isRunning", Horizontal != 0.0f);

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Debug.DrawRay(transform.position, Vector3.down * 2f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 2f))
        {
            Grounded = true;
        }
        else Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * Jumpforce);
    }

    public void Hit()
    {
        if (!isInvulnerable) // Verificar si el jugador es vulnerable
        {
            Health -= 1;
            Debug.Log("Recibió daño. Vidas restantes: " + Health);

            if (Health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                StartCoroutine(InvulnerabilityTimer()); // Activar la invulnerabilidad temporal
            }
        }
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true; // Hacer que el jugador sea invulnerable
        yield return new WaitForSeconds(invulnerableDuration); // Esperar por la duración de invulnerabilidad
        isInvulnerable = false; // Desactivar la invulnerabilidad
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bala"))
        {
            Hit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Hit();
        }
    }
}
