using System;
using System.Collections;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;

    [Tooltip("Health at which this object should have in the start")]
    [SerializeField] private float startHealth = 100f;
    [SerializeField] private GameObject floatingText;
    private float currentHealth;


    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

        }
    }

    private bool shoulDie => CurrentHealth == 0;




    public Action<float> OnTakeDamage;
    public Action<float> OnHeal;
    public Action<float> OnMaxHealthIncrease;
    public Action OnDie;

    private void Awake()
    {
        startHealth = Mathf.Clamp(startHealth, 0, maxHealth);
        CurrentHealth = startHealth;
    }
    private IEnumerator ShowDelayedFloatingText(float damageAmount)
    {
        // Create a new instance of the floating text object
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform.parent);

        // Set the text to the damage amount
        go.GetComponent<TextMesh>().text = damageAmount.ToString();

        // Wait for 1 second
        yield return new WaitForSeconds(6f);

        // Destroy the floating text object after the delay
        Destroy(go);
    }

    private void ShowFloatingText(float damageAmount)
    {
        // Start the coroutine
        StartCoroutine(ShowDelayedFloatingText(damageAmount));
    }


    public void TakeDamage(float damageAmt)
    {
        CurrentHealth -= damageAmt;
        OnTakeDamage?.Invoke(currentHealth);
         ShowFloatingText(damageAmt);
     
        //Debug.Log("Current health of enemy is: " + CurrentHealth);
        if (shoulDie)
        {
            Die();
        }



    }
  
    public void Heal(float healAmt)
    {

    }

    public void IncraseMaxxHealth()
    {

    }

    private void Die()
    {
        //things that can be done before dying

        OnDie?.Invoke();
    }

    private void OnEnable()
    {
        CurrentHealth = startHealth;
    }
}
