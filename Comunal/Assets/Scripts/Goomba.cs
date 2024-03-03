using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public Sprite flatSprite;
    private void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.CompareTag("Player")){
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
           if(col.transform.Test(transform, Vector2.down)){
                Flatten();
            }else{
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.layer == LayerMask.NameToLayer("Shell")){
            GetComponent<Death>().enabled = true;
            Destroy(gameObject, 3f);
        }
    }

    private void Flatten(){
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        //GetComponent<AnimationController>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = flatSprite;
        Destroy(gameObject, 0.5f);
    }
}
