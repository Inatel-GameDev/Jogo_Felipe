using System.Collections;
using UnityEngine;

public abstract class BlockController : MonoBehaviour
{
    public Sprite hitSprite;
    private bool animando;
    private bool hasbeenhit;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player")){
            if(col.transform.Test(transform, Vector2.up)){
                BlockHit(); 
            }
        }
    }

    public virtual void BlockHit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = hitSprite;
        if(!animando && !hasbeenhit)
            StartCoroutine(AnimateBlock());
        hasbeenhit = true;
    }

    private IEnumerator AnimateBlock(){
        animando = true;
        Vector3 pos = transform.localPosition;
        Vector3 finalpos = pos + Vector3.up/2;
        yield return Move(pos, finalpos);
        yield return Move(finalpos, pos);
        animando = false;


    }

    private IEnumerator Move(Vector3 inicial, Vector3 final){
        float time = 0f;
        float duration = 0.125f;

        while(time < duration){
            float porcentagem = time/duration; //valor de 0% a 100% da animação
            transform.localPosition = Vector3.Lerp(inicial, final, porcentagem);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = final;
    }
}
