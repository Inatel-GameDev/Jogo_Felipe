using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject health;
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            Debug.Log("ganha vida");
            Instantiate(health, transform.position, Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.K)){
            Debug.Log("perde vida");
            var i = gameObject.transform.childCount - 1;
            if(i != 0){
                Destroy(gameObject.transform.GetChild(i).gameObject);
            }
        }
    }
}
