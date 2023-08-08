using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    private Vector2 direction; // Direcci�n de movimiento

    public float tiempoVida = 3f; // Tiempo de vida de la bala

    private void Start()
    {
        // La direcci�n de la bala es hacia la derecha (eje X)
        direction = Vector2.right; // Cambia Vector2.right a la direcci�n deseada

        // Iniciar la rutina para destruir la bala despu�s de tiempoVida segundos
        StartCoroutine(DestruirDespuesDeTiempo(tiempoVida));
    }

    private void Update()
    {
        // Mover la bala en la direcci�n y velocidad especificadas
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private IEnumerator DestruirDespuesDeTiempo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Destruir la bala despu�s de tiempoVida segundos
    }
}


