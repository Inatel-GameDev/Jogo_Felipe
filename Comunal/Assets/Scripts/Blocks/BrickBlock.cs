using System;
using System.Collections;
using UnityEngine;

public class BrickBlock : BlockController
{
    public void Start(){
        EventManager.Instance.OnEventTriggered += BlockHit;
        //bomb.onExplode += BlockHit;
    }

    public override void BlockHit()
    {
        Debug.Log("bombhit");
        base.BlockHit();
        Destroy(gameObject);
    }

}
