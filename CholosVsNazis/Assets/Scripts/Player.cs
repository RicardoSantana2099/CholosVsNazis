using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    private float Horizontal;
    private bool Grounded;
    private int Health = 3;
   

    public GameObject BulletPrefab;
    public float Jumpforce;
    public float Speed;
    
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
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

        if(Input.GetKeyDown(KeyCode.W) && Grounded)
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
        Health = Health - 1;
        if (Health == 0) Destroy(gameObject); 
    }
}
