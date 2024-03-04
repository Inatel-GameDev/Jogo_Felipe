using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AnimationController anim;
    private PlayerController player;
    //private Anim run;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GetComponent<PlayerController>();
        anim = GetComponent<AnimationController>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    private void LateUpdate(){
        if(player.jumping)
            anim.ChangeAnimationState("Jump");
        else if(player.running)
            anim.ChangeAnimationState("Run");
        else if(player.sliding)
            anim.ChangeAnimationState("Slide");
        else if(player.charging)
            anim.ChangeAnimationState("Charge");
        else
            anim.ChangeAnimationState("Idle");
    }
}
