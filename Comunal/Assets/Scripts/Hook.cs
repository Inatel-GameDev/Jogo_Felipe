using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private PlayerController player;
        public float distancia;
    public float speed;
    // Start is called before the first frame update

    public void OnEnable(){
        distancia = 0f;
        player = GetComponentInParent<PlayerController>();
    }

    public void Update(){
        if(player.state == PlayerController.PlayerState.Charging){
            distancia += Time.deltaTime * 7;
            distancia = Math.Min(distancia, 8f);
        }
        else if (player.state == PlayerController.PlayerState.Throwing){
            StartCoroutine(Throw());
        }
    }

    public IEnumerator Throw(){
        Vector3 pos = transform.localPosition;
        Vector3 finalpos = pos + new Vector3(distancia, 0 ,0); //v = d/s v/d = 1/s  d/5 = s
        yield return Move(pos, finalpos);
                yield return Move(finalpos, pos);
        gameObject.SetActive(false);
        player.state = PlayerController.PlayerState.FreePlay;

    }
    private IEnumerator Move(Vector3 inicial, Vector3 final){
        float time = 0f;
        float duration = distancia/speed;

        while(time < duration){
            float porcentagem = time/duration; //valor de 0% a 100% da animação
            transform.localPosition = Vector3.Lerp(inicial, final, porcentagem);    
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = final;
    }
}
