using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerroMalo : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;

    private int direccion = -1; // Iniciar hacia la izquierda
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * direccion * velocidadMovimiento * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            // Desactivar el sprite y el collider al tocar la pared
            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else if (collision.gameObject.CompareTag("Enemigo"))
        {
            // Cambiar de lugar con el enemigo al tocarlo
            Vector3 tempPosition = transform.position;
            transform.position = collision.transform.position;
            collision.transform.position = tempPosition;
        }
    }
}

