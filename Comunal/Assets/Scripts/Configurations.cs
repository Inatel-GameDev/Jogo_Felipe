using System.Collections;
using UnityEngine;

public static class Configurations
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");

    public static bool Raycast(this Rigidbody2D rb2d, Vector2 dir){
        if(rb2d.isKinematic){
            return false;
        }
        float raio = 0.25f;
        float dist = 0.375f;

        RaycastHit2D hit = Physics2D.CircleCast(rb2d.position, raio, dir.normalized, dist, layerMask);
        return hit.collider != null && hit.rigidbody != rb2d;
    }

    public static bool Test(this Transform transform, Transform other, Vector2 testDirection){
        Vector2 direcao = other.position - transform.position;
        return Vector2.Dot(direcao.normalized,testDirection) > 0.25f;
    }
}