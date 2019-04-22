using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform projectileSpawnpoint;
    public GameObject projectile;
    public LayerMask playerLayer;
    //cut this value from the platform dimension
    public float platformMovementOffset;
    public float speed;
    private int direction = 1; // 1 = right; -1 = left
    private bool movable = false;
    private float platformFarLeftPoint;
    private float platformFarRightPoint;
    private bool stuckInPlace = false;

    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        platformFarLeftPoint = collision.gameObject.GetComponent<BoxCollider2D>().bounds.min.x;
        platformFarRightPoint = collision.gameObject.GetComponent<BoxCollider2D>().bounds.max.x;
        movable = true;
    }



    private void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

        float leftRayDistance = transform.position.x - platformFarLeftPoint;
        float rightRayDistance = transform.position.x - platformFarRightPoint;
        Debug.DrawRay(transform.position, Vector2.left * leftRayDistance, Color.red);
        Debug.DrawRay(transform.position, Vector2.left * rightRayDistance, Color.green);
        RaycastHit2D raycastHit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, leftRayDistance, playerLayer);
        RaycastHit2D raycastHit2DRight = Physics2D.Raycast(transform.position, Vector2.left, rightRayDistance, playerLayer);
        if (raycastHit2DLeft && myTime > nextFire)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            Instantiate(projectile, projectileSpawnpoint.position,Quaternion.identity);
            stuckInPlace = true;
            nextFire = myTime + fireDelta;
            nextFire = nextFire - myTime;
            myTime = 0.0F;
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        if (raycastHit2DRight && myTime > nextFire)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(projectile, projectileSpawnpoint.position, Quaternion.identity);
            stuckInPlace = true;
            nextFire = myTime + fireDelta;
            nextFire = nextFire - myTime;
            myTime = 0.0F;
            gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
        }
        myTime = myTime + Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(movable && !stuckInPlace)
        {
            if (transform.position.x <= platformFarLeftPoint + platformMovementOffset)
            {
                direction = 1;
            } else if (transform.position.x >= platformFarRightPoint - platformMovementOffset)
            {
                direction = -1;
            }
            transform.Translate(direction * speed * Vector3.right * Time.deltaTime, Space.Self);
        }
    }

}
