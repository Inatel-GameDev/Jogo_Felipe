using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float smoothing;
    public Vector2 minPos;
    public Vector2 maxPos;

    void LateUpdate()
    {
        if(transform.position != target.position){
           Vector3 targetPosition = new Vector3(target.position.x, target.position.y, -10);

           targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
           targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

    }
}
