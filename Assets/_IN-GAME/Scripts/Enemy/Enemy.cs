using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Transform playerTransform;

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
        //Rotating the enemy in the player direction
        transform.LookAt(playerTransform.position);


        Vector3 distance = transform.position - playerTransform.position;
        distance = distance.normalized;
        transform.position = Vector2.Lerp(transform.position,distance,moveSpeed * Time.deltaTime);


    }

}
