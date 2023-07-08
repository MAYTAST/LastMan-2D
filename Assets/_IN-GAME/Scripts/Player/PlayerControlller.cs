using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlller : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed as needed
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] PowerManager PowerManager;
    private Rigidbody2D rb;

    private Vector2 movement;

    //Public properties 
    public bool IsMoving => movement == Vector2.zero;


 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    private void Update()
    {
        // Input
        movement = joystick.Direction.normalized;
     
      
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "KillAll":
                if (PowerManager != null)
                {
                    Destroy(collision.gameObject);
                    PowerManager.KillAllEnemies();
                }
                break;
            case "CollectAll":
                if (PowerManager != null)
                {
                    Destroy(collision.gameObject);
                    PowerManager.CollectAll();
                }
                break;
            default:
                break;
        }
    }
}
