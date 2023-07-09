using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlller : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the movement speed as needed
    [SerializeField] private DynamicJoystick joystick;
    [SerializeField] PowerManager PowerManager;
    private Rigidbody2D rb;

    private Vector2 movement;

    private Vector2 lastLookDir;

    //Public properties 
    public bool IsMoving => movement != Vector2.zero;
    
    /// <summary>
    /// The direction in which the player is looking (already normalized)
    /// </summary>
    public Vector2 LookDir
    {
        get;
        private set;
    }


 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    private void Update()
    {
        // Input
        movement = joystick.Direction.normalized;
        if (IsMoving)
        {
            LookDir = joystick.Direction;
            lastLookDir = LookDir;
        }
        else
        {
            LookDir = lastLookDir;
        }
       
      
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

    private void OnDrawGizmos()
    {
       Debug.DrawRay(transform.position,LookDir * 3f,Color.black);
    }
}
