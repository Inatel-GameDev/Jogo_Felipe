using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 input;
    private Vector2 velocity;
    private Rigidbody2D rb2d;

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private Death death;


    float moveSpeed = 8;
    public float puloMax = 5f;
    public float jumpForce => 2f * puloMax/0.5f; //getter only propriety
    public float gravidade => -2f * puloMax/Mathf.Pow(0.5f,2);

    public bool grounded {get; private set;}
    public bool jumping {get; private set;}
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(input.x) > 0.25f;
    public bool sliding => (input.x > 0f && velocity.x < 0f) || (input.x < 0f && velocity.x > 0f);

    public bool big => bigRenderer.enabled;
    public bool dead => death.enabled;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        death = GetComponent<Death>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = rb2d.Raycast(Vector2.down);
        if(grounded){
            GroundedMovement();
        }

        Gravity();

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        velocity.x = Mathf.MoveTowards(velocity.x, input.x * moveSpeed, moveSpeed * 2 * Time.deltaTime);

        if(rb2d.Raycast(Vector2.right * velocity.x)){ //Checa se tem alguma coisa no bloco a direita sem precisar de collider :)
            velocity.x = 0f;
        }

        if(velocity.x > 0f){
            transform.localScale = new Vector2(1, 1);
        } else if(velocity.x < 0f){
            transform.localScale = new Vector2(-1, 1);
        }

        if(transform.position.y < -6){
            Die();
        }
    }

    private void FixedUpdate(){
        Vector2 pos = rb2d.position;
        pos += velocity * Time.deltaTime;
        rb2d.MovePosition(pos);
        
    }

    private void GroundedMovement(){
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f; //jeito mais bonito de fazer uma condição booleana, lembrar pra o futuro | bool = (resultado booleano da expressão)

        if (Input.GetKey(KeyCode.Space))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void Gravity(){
        bool falling = velocity.y < 0f || !Input.GetKey(KeyCode.Space); // durante o pulo, falling não ativa enquanto vc n soltar o botao 
        float mult = falling ? 2f : 1f;
        
        velocity.y += gravidade * mult * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravidade/2); // valor minimo da aceleracao vertical é metade da gravidade 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(transform.Test(col.transform, Vector2.up)){
            Debug.Log("bateu a cabeca");
            velocity.y = 0;
        }

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.Test(col.transform, Vector2.down)){
                velocity.y = jumpForce/2f;
                jumping = true;
            }
        }
        else if (col.gameObject.CompareTag("PowerUp"))
        {
            PowerUp();
        }else{
            if(col.transform.Test(transform, Vector2.up)){
                velocity.y = 0;
            }
        }
    }

    void PowerUp()
    {
        //BigMario = true;
    }

    public void Hit()
    {
        Debug.Log("Hit");
        if(big){
            Shrink();
        }else{
            Die();
        }
    }

    public void Shrink(){

    }

    void Die()
    {
        smallRenderer.enabled = false;
        bigRenderer.enabled = false;
        death.enabled = true;

        GameController.Instance.ResetLevel(3f);
    }
}
