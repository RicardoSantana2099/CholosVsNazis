using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    private Vector2 direction; // Dirección de movimiento

    public float tiempoVida = 3f; // Tiempo de vida de la bala

    private GameObject player; // Referencia al jugador

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // Encontrar el jugador

        if (player != null)
        {
            // Calcular la dirección hacia el jugador desde la posición de la bala
            direction = (player.transform.position - transform.position).normalized;
        }
        else
        {
            // Si no se encuentra el jugador, moverse hacia la derecha por defecto
            direction = Vector2.right;
        }

        // Iniciar la rutina para destruir la bala después de tiempoVida segundos
        StartCoroutine(DestruirDespuesDeTiempo(tiempoVida));
    }

    private void Update()
    {
        // Mover la bala en la dirección calculada y a la velocidad especificada
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private IEnumerator DestruirDespuesDeTiempo(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // Destruir la bala después de tiempoVida segundos
    }
}


