using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlller : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Adjust the movement speed as needed
    [SerializeField] private FixedJoystick joystick;

    private Rigidbody2D rb;

    private Vector2 movement;

    private Animator anim;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Input
        movement = joystick.Direction.normalized;
     
      //  Debug.Log(movement.x);

      //-> movement Animation 
        bool isIdle = movement.x == 0 && movement.y == 0;

        if (isIdle)
        {
            anim.SetBool("isMoving", false);
        }
        else
        {
            anim.SetFloat("HorizontalMovement", movement.x);

            anim.SetBool("isMoving", true);
            anim.SetFloat("VerticalMovement", movement.y);
        }
      
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
