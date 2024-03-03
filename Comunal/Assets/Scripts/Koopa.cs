using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public Sprite shellSprite;
    private bool shell;
    private bool isMoving;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!shell && col.gameObject.CompareTag("Player")){
            PlayerController player = col.gameObject.GetComponent<PlayerController>();
            if(col.transform.Test(transform, Vector2.down)){
                EnterShell();
            }else{
                player.Hit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(shell && col.CompareTag("Player")){
            if(!isMoving){
                Vector2 dir = new(transform.position.x - col.transform.position.x, 0f);
                isMoving = true;
                GetComponent<Rigidbody2D>().isKinematic = false;
                EnemyController enemy = GetComponent<EnemyController>();
                enemy.dir = dir.normalized;
                enemy.moveSpeed = 12;
                enemy.enabled = true;
                gameObject.layer = LayerMask.NameToLayer("Shell");
            }else{
                PlayerController player = col.gameObject.GetComponent<PlayerController>();
                player.Hit();
            }
        }
        if(col.gameObject.layer == LayerMask.NameToLayer("Shell")){
            GetComponent<Death>().enabled = true;
            Destroy(gameObject, 3f);
        }
    }

    private void EnterShell()
    {
        shell = true;
        //GetComponent<AnimationController>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = shellSprite;
    }
}
