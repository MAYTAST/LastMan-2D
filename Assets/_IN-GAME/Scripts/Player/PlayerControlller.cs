using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlller : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed as needed

    private Rigidbody2D rb;

    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
