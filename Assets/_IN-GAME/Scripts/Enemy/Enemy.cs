using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;
    public float attackRange = 2f;
    public Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = playerTransform.position - transform.position;
        float distanceX = direction.x;
        direction.y += 0.5f; // Adjust the Y-component of the direction

        direction.Normalize();

       

        if (direction.x < 0f)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0f)
        {
            spriteRenderer.flipX = false;
        }

        if (Mathf.Abs(distanceX) <= attackRange)
        {
            // Play attack animation
            animator.SetBool("CanAttack", true);
        }
        else
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            animator.SetBool("CanAttack", false);
        }
    }

}
