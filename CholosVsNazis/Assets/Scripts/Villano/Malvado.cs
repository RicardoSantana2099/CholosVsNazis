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
    [SerializeField] private float vida = 1f; // Agregar vida

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

        // Verificar si el jugador está en línea de visión antes de disparar
        if (!estaAtacando && PuedeVerJugador())
        {
            Disparar();
        }
    }

    private bool PuedeVerJugador()
    {
        Vector2 playerDirection = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection, 10f); // Distancia de visión de 10 unidades

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true; // El Malvado puede ver al jugador
        }

        return false; // No hay línea de visión clara al jugador
    }

    private void Disparar()
    {
        if (!haDisparado)
        {
            animator.SetBool("Ataque", true); // Cambiar el parámetro a true
            estaAtacando = true;
            Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);
            Instantiate(proyectilPrefab, puntoDisparo.position + new Vector3(4f, 0f, 0f), Quaternion.identity); // Disparar segunda bala con diferencia de 4 unidades en el eje X
            Instantiate(proyectilPrefab, puntoDisparo.position + new Vector3(8f, 0f, 0f), Quaternion.identity); // Disparar tercera bala con diferencia de 8 unidades en el eje X

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
        spriteRenderer.flipX = direccion == 1 ? false : true;
    }

    public void TomarDaño(float cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Aquí puedes realizar acciones adicionales cuando el enemigo muere
        Destroy(gameObject); // Por ejemplo, aquí simplemente destruimos el objeto
    }
}






