using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillanoFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform player;

    private bool isFacingRight = true;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);

    }

    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight)|| (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player Hector = collision.collider.GetComponent<Player>();
        if(Hector != null)
        {
            Hector.Hit();
        }
    }
}
