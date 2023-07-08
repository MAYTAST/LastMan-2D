using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float attackRate = 2f;

    [Header("References")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletParent;


    [Header("Enemy detcction")]
    [SerializeField,Range(10f,30f)] private float detectRange = 20f;
    [SerializeField] private LayerMask enemyLayer;


    private float lastTimeFire = 0f;
    private PlayerControlller controller;


    private void Awake()
    {
        controller = GetComponent<PlayerControlller>();
        if(bulletParent == null)
        {

        }
    }
    private void Update()
    {
        if(!controller.IsMoving && ShouldShoot())
        {
            Shoot();
        }
    }


    private bool ShouldShoot()
    {
        return Time.time <= attackRate + lastTimeFire;
    }

    /*private Transform FindTheNearestTarget()
    {

    }*/

    private void Shoot()
    {
        //Shooting logic
        Instantiate(bulletPrefab, attackPoint.position, transform.rotation, bulletParent);

        lastTimeFire = Time.time;
    }


    private void OnValidate()
    {
        if(bulletParent == null)
        {
            bulletParent = transform;
        }    
    }
}
