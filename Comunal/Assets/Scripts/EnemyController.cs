using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int moveSpeed;
    public Vector2 dir = Vector2.left;
    private Vector2 velocity;
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        enabled = false;
    }

    private void OnBecameVisible(){
        enabled = true;
    }

    private void OnBecameInvisible(){
        enabled = false;
    }

    private void OnEnable(){
        rb2d.WakeUp();
    }
    private void OnDisable(){
        rb2d.velocity = Vector2.zero;
        rb2d.Sleep();
    }

    private void FixedUpdate(){
        velocity.x = dir.x * moveSpeed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);

        if(rb2d.Raycast(dir)){
            dir = -dir;
        }

        if(rb2d.Raycast(Vector2.down)){
            velocity.y = Mathf.Max(velocity.y, 0f); //velocidade 0 ou maior que zero (subindo)
        }
    }
}
