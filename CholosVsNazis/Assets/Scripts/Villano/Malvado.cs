using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malvado : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float rangoDisparo;
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private float tiempoQuietoDespuesDisparo;

    private int direccion = 1; // 1 para derecha, -1 para izquierda
    private SpriteRenderer spriteRenderer;
    private bool haDisparado = false;
    private bool enTiempoQuieto = false;
    private bool estaAtacando = false; // Nuevo parámetro para el estado de ataque
    private Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    private void Update()
    {
        if (enTiempoQuieto)
        {
            return;
        }

        transform.Translate(Vector3.right * direccion * velocidadMovimiento * Time.deltaTime);

        // Disparar si está en rango y no ha disparado antes
        if (!haDisparado && EstaEnRango())
        {
            Disparar();
        }
    }

    private bool EstaEnRango()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rangoDisparo);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }

    private void Disparar()
    {
        if (!haDisparado)
        {
            animator.SetBool("Ataque", true); // Cambiar el parámetro a true
            estaAtacando = true;
            Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            StartCoroutine(TiempoQuieto());
        }
    }

    private IEnumerator TiempoQuieto()
    {
        enTiempoQuieto = true;
        yield return new WaitForSeconds(tiempoQuietoDespuesDisparo);
        enTiempoQuieto = false;
        CambiarDireccion();
        animator.SetBool("Ataque", false); // Cambiar el parámetro a false
        estaAtacando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            CambiarDireccion();
        }
    }

    private void CambiarDireccion()
    {
        direccion *= -1; // Cambiar la dirección de movimiento

        // Girar hacia la nueva dirección del movimiento
        if (direccion == 1)
        {
            spriteRenderer.flipX = false; // No voltear horizontalmente
        }
        else
        {
            spriteRenderer.flipX = true; // Voltear horizontalmente
        }
    }
}


