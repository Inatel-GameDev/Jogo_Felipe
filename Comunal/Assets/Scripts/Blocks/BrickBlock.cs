using System.Collections;
using UnityEngine;

public class BrickBlock : BlockController
{
    public override void BlockHit()
    {
        base.BlockHit();
        Debug.Log("Brick Block");
    }

}
