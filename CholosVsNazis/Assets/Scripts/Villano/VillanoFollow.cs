using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillanoFollow : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [SerializeField] private float vida;
    private Animator animator;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask queEsSuelo;

    private bool isDead = false;  // Nuevo flag para controlar si el villano ha muerto

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isDead)  // Evitar que el villano se mueva si está muerto
        {
            rb2D.velocity = new Vector2(velocidadMovimiento * transform.right.x, rb2D.velocity.y);

            RaycastHit2D informacionSuelo = Physics2D.Raycast(transform.position, transform.right, distancia, queEsSuelo);

            if (informacionSuelo.collider == null)
            {
                Girar();
            }
        }
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * distancia);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0 && !isDead)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        isDead = true;  // Marcar al villano como muerto para detener su movimiento

        // Retroceso y parpadeo
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        // Retroceso
        rb2D.velocity = new Vector2(-velocidadMovimiento * transform.right.x, rb2D.velocity.y);

        // Parpadeo durante la animación de muerte
        float blinkDuration = 1.0f;  // Duración del parpadeo en segundos
        float blinkInterval = 0.1f;  // Intervalo entre cambios de visibilidad
        int blinkCount = Mathf.FloorToInt(blinkDuration / blinkInterval);

        for (int i = 0; i < blinkCount; i++)
        {
            // Cambiar la visibilidad del villano
            gameObject.SetActive(!gameObject.activeSelf);

            yield return new WaitForSeconds(blinkInterval);
        }

        // Asegurarse de que el villano sea visible al final
        gameObject.SetActive(true);

        // Eliminar el objeto después de la animación de muerte
        Destroy(gameObject);
    }
}
