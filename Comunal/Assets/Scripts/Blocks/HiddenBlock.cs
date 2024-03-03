using System.Collections;
using UnityEngine;

public class HiddenBlock : BlockController
{
    private SpriteRenderer spriteRenderer;
    private void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    public override void BlockHit()
    {
        spriteRenderer.enabled = true;
        base.BlockHit();
        Debug.Log("Hidden Block");
    }

}

