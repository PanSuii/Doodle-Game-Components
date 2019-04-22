using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float movementSpeed;
    public float jumpForce;
    private float horizontalMovement;
    private bool jump = false;

    public float jumpDelta = 0.5F;
    private float nextJump = 0.5F;
    private float myTime = 0.0F;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        myTime = myTime + Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb2D.AddForce(new Vector2(horizontalMovement * movementSpeed, 0));
        if (jump && myTime > nextJump)
        {
            jump = false;
            rb2D.AddForce(new Vector2(0, jumpForce));
            nextJump = myTime + jumpDelta;
            nextJump = nextJump - myTime;
            myTime = 0.0F;
        }
    }

}