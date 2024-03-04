using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float timer;

    public void Update(){
        timer -= Time.deltaTime;
        if(timer == 0f){
            Explosion();
        }
    }

    public void Explosion(){
        Debug.Log("boom");
        EventManager.Instance.TriggerEvent();
    }
}
