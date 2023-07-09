using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private float currentSpeed;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float circleRadius = 2f; // Adjust the radius of the overlapping circle
    public LayerMask playerLayer;
    private bool isPlayerInRange = false;

    private Vector3 direction;
    private void Start()
    {
        currentSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        MoveTowardsPlayer();
        Flip();
        DetectPlayerAndAttack();
    }

    private void Flip()
    {
        if (direction.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void DetectPlayerAndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, circleRadius, playerLayer);
        bool isPlayerDetected = colliders.Length > 0;

        if (isPlayerDetected && !isPlayerInRange)
        {
            // Player entered the attack range
            isPlayerInRange = true;
            currentSpeed = 0;
            // Play attack animation
            animator.SetBool("CanAttack", true);
        }
        else if (!isPlayerDetected && isPlayerInRange)
        {
            // Player exited the attack range
            isPlayerInRange = false;
            currentSpeed = moveSpeed;
            // Reset the attack animation
            animator.SetBool("CanAttack", false);
        }
    }
    private void MoveTowardsPlayer()
    {
        direction = playerTransform.position - transform.position;
        float distanceX = direction.x;
        direction.y += 0.5f; // Adjust the Y-component of the direction

        direction.Normalize();
      
       // Debug.Log(playerTransform.position.x);
        transform.Translate(direction * currentSpeed * Time.deltaTime);
      

       

    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the overlapping circle in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

  
}
