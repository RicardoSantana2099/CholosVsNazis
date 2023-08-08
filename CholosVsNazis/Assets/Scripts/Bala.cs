using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    private Vector2 direction; // Direcci�n de movimiento

    public float tiempoVida = 3f; // Tiempo de vida de la bala

    private GameObject player; // Referencia al jugador

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Encontrar el jugador

        if (player != null)
        {
            // Calcular la direcci�n hacia el jugador desde la posici�n de la bala
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // Si no se encuentra el jugador, moverse hacia la derecha por defecto
            direction = Vector2.right;
        }

        // Iniciar la rutina para destruir la bala despu�s de tiempoVida segundos
        StartCoroutine(DestruirDespuesDeTiempo(tiempoVida));
    }

    private void Update()
    {
        // Mover la bala en la direcci�n calculada y a la velocidad especificada
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private IEnumerator DestruirDespuesDeTiempo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Destruir la bala despu�s de tiempoVida segundos
    }
}


