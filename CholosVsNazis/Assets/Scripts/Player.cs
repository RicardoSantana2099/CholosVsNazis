using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float Horizontal;
    private bool Grounded;
    private int Health = 5;
    private bool isInvulnerable = false;
    public float invulnerableDuration = 1.0f;

    [SerializeField]private BarraDeVida barraDeVida;

    public GameObject BulletPrefab;
    public float Jumpforce;
    public float Speed;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        barraDeVida.InicializarBarraDeVida(Health);
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
        if (!isInvulnerable)
        {
            Health -= 1;
            Debug.Log("Recibió daño. Vidas restantes: " + Health);

            barraDeVida.CambiarVidaActual(Health);

            if (Health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                StartCoroutine(InvulnerabilityTimer());
            }
        }
    }

    private IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableDuration);
        isInvulnerable = false;
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
