using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public event Action OnEventTriggered;

    private void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    public void TriggerEvent(){
        OnEventTriggered?.Invoke();
    }
}