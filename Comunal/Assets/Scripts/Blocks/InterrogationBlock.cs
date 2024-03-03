using System.Collections;
using UnityEngine;

public class InterrogationBlock : BlockController
{
    public GameObject Item;
    public override void BlockHit()
    {
        base.BlockHit();
        Debug.Log("Interrogation Block");
        if(Item != null){
            Instantiate(Item, transform.position, Quaternion.identity);
        }
    }

}

