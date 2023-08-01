using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed;
    private Rigidbody2D Rigidbody2D;
    private Vector2 Direction;
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

   
    void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }
}
