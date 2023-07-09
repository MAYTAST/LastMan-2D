using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] float damageAmount = 10f;

    public float circleRadius = 2f; // Adjust the radius of the overlapping circle
    public LayerMask playerLayer;
    public float damageDuration = 1.0f; // The duration over which the damage is applied



    private float damageTimer = 0.0f; // Timer to track the damage duration
    private bool isDamaging = false;
    private float currentSpeed;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isPlayerInRange = false;
    private Vector3 direction;
    private PlayerEntity PlayerEnity;


    private void Start()
    {
        currentSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
        PlayerEnity = playerTransform.GetComponent<PlayerEntity>();
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
        DoDamage();
       
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
         
           
            animator.SetBool("CanAttack", false);
            isDamaging = false;
            damageTimer = 0.0f;

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
    private void DoDamage()
    {
        if (isPlayerInRange)
        {
            if (!isDamaging)
            {
                // Start applying damage
                damageTimer = 0.0f;
                isDamaging = true;
            }
            else
            {
                // Continue applying damage gradually
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageDuration)
                {
                    // Damage duration has passed, stop applying damagez
                    isDamaging = false;
                    damageTimer = 0.0f;
                }
                else
                {
                    // Apply a fraction of the total damage over time
                    float damageFraction = Time.deltaTime / damageDuration;
                    float damageAmountPerFrame = damageAmount * damageFraction;
                    PlayerEnity.TakeDamage(damageAmountPerFrame);
                }
            }
        }
        else
        {
            // Player is out of range, stop applying damage
            isDamaging = false;
            damageTimer = 0.0f;
        }

        Debug.Log(playerTransform.GetComponent<PlayerEntity>().CurrentHealth);
    }
    private void OnDrawGizmosSelected()
    {
        // Visualize the overlapping circle in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

  
}
