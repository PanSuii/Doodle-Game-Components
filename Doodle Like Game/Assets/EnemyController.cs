using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LayerMask layer;
    private bool isGrounded;

    private void Update()
    {
        Ray2D ray2D = new Ray2D(transform.position, new Vector2(1, -1));
        Debug.DrawRay(ray2D.origin, ray2D.direction);
        RaycastHit2D raycastHit = Physics2D.Raycast(ray2D.origin, ray2D.direction, 1, layer.value);
        Debug.Log(raycastHit.collider.name);
    }
}
