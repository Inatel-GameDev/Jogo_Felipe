using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cogumelo : MonoBehaviour
{
    public bool moving = true;
    public int moveSpeed;
    private Rigidbody2D rb2d;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 2f, 6);
        if(collider != null){
            Debug.Log("COGUMELO");
        }

        if(moving){
            Move();
        }
    }

    private void Move(){
        Vector2 vel = new(0, rb2d.velocity.y)
        {
            x = moveSpeed
        };
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1);
       
        rb2d.velocity = vel;
    }

    private void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Blocks") && moving)
        {
            moveSpeed = -moveSpeed;
        }
        if(col.gameObject.CompareTag("Player") && moving){
            Debug.Log("Power Up");
            Destroy(gameObject);
        }
    }
}
