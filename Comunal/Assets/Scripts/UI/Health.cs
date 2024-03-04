using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Transform HealthBar;

    public void Awake(){
        gameObject.transform.SetParent(HealthBar);
        gameObject.transform.localScale = new Vector3(1,1,1);
    }
}
