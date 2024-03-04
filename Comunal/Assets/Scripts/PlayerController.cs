using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState{
        FreePlay,
        Charging,
        Throwing,
        Cutscene
    }
    public PlayerState state;
    private Vector2 input;
    private Vector2 velocity;
    private Rigidbody2D rb2d;

    public PlayerSpriteRenderer smallRenderer;
    public PlayerSpriteRenderer bigRenderer;
    private Death death;
    public Hook hook;
    float moveSpeed = 8;
    public float puloMax = 5f;
    public float jumpForce => 2f * puloMax/0.5f; //getter only propriety
    public float gravidade => -2f * puloMax/Mathf.Pow(0.5f,2);

    public bool grounded {get; private set;}
    public bool jumping {get; private set;}
    public bool charging {get; private set;}
    public bool throwing {get; private set;}
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(input.x) > 0.25f;
    public bool sliding => (input.x > 0f && velocity.x < 0f) || (input.x < 0f && velocity.x > 0f);
    public bool dead => death.enabled;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        death = GetComponent<Death>();
        state = PlayerState.FreePlay;
    }

    // Update is called once per frame
    public void Update()
    {
        grounded = rb2d.Raycast(Vector2.down);
        if(grounded){
            GroundedMovement();
        }

        Gravity();

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(state == PlayerState.FreePlay){
            velocity.x = Mathf.MoveTowards(velocity.x, input.x * moveSpeed, moveSpeed * 2 * Time.deltaTime);
        }
        if(state == PlayerState.Charging){
            hook.gameObject.SetActive(true);
            velocity.x = Mathf.MoveTowards(velocity.x, input.x * moveSpeed/2, moveSpeed * 2 * Time.deltaTime);
            if(Input.GetKeyUp(KeyCode.Z)){

                state = PlayerState.Throwing;
            }
        }
        else if(state == PlayerState.Throwing){
            velocity.x = Mathf.MoveTowards(velocity.x, 0, moveSpeed * 2 * Time.deltaTime);
        }

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
            jumping = true;
        }
        if(Input.GetKey(KeyCode.Z) && !throwing){
            state = PlayerState.Charging;
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

        if(col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(transform.Test(col.transform, Vector2.down)){
                velocity.y = jumpForce/2f;
                jumping = true;
            }
        }
        else{
            if(col.transform.Test(transform, Vector2.up)){
                velocity.y = 0;
            }
        }
    }

    void Interact()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 2f);
        if(collider != null){
            collider.GetComponent<NPCController>()?.Interact(transform);
        }
    }

    public void Hit()
    {
        Debug.Log("Hit");
    }

    void Die()
    {
        death.enabled = true;

        GameController.Instance.ResetLevel(3f);
    }
}
