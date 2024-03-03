using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite deathSprite;

    private void OnEnable(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(AnimateDeath());
    }

    private void UpdateSprite(){
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
        if(deathSprite != null)
            spriteRenderer.sprite = deathSprite;
    }

    private void DisablePhysics(){
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (var item in cols)
        {
            item.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true;

        PlayerController playerController = GetComponent<PlayerController>();
        EnemyController enemyController = GetComponent<EnemyController>();
        if(playerController != null)
            playerController.enabled = false;
        if(enemyController != null)
            enemyController.enabled = false;
    }

    private IEnumerator AnimateDeath(){
        float elap = 0f;
        float duracao =  3f;

        Vector3 velocity = Vector3.up * 10f;
        while(elap < duracao){
            transform.position += velocity * Time.deltaTime;
            velocity.y += -35f * Time.deltaTime;
            elap += Time.deltaTime;
            yield return null;
        }
    }
}
