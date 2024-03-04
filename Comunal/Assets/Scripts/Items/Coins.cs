using System.Collections;
using UnityEngine;

public class Coins : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    float framerate = 1/6f;
    private int frame;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable(){
        InvokeRepeating(nameof(IdleCoin), framerate, framerate);
    }

    private void IdleCoin(){
        frame++;
        if(frame >=sprites.Length){
            frame = 0;
        }
        if(frame >= 0 && frame < sprites.Length){
            spriteRenderer.sprite = sprites[frame];
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.CompareTag("Player")){
            Collected();
        }
    }

    private void OnCollisionStay2D(Collision2D col){
        if(col.gameObject.CompareTag("Hook")){
            transform.position = col.transform.position;
        }
    }

    public void Collected(){
        //CancelInvoke();
        StartCoroutine(AnimateCoin());
    }

    private IEnumerator AnimateCoin(){
        Vector3 pos = transform.localPosition;
        Vector3 finalpos = pos + Vector3.up*2;
        yield return Move(pos, finalpos);
        Destroy(gameObject);
        GameController.Instance.AddCoin();
    }

    private IEnumerator Move(Vector3 inicial, Vector3 final){
        float time = 0f;
        float duration = 0.25f;

        while(time < duration){
            float porcentagem = time/duration; //valor de 0% a 100% da animação
            transform.localPosition = Vector3.Lerp(inicial, final, porcentagem);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = final;
    }
}
