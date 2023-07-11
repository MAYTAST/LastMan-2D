using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Sprites;
using DG.Tweening;
public enum Power
{
 KillAll,
 CollectAll

}
[System.Serializable]
public class PowerClass
{
    public Power Power;
    public GameObject _PowerPrefab;
    public Sprite _afterDrop;
}
public class PowerManager : MonoBehaviour
{
    [Header("Kill-All")]
    public PowerClass _KillAll;
    [Header("Collect-All")]
    public PowerClass _CollectAll;
    [Header("SpawnValues")]
    public int SpawnRangeMinX;
    public int SpawnRangeMaxX;
    public int SpawnRangeMinY;
    public int SpawnRangeMaxY;
    [Header("Values")]
    public float waitTimeToVansish;


    [SerializeField] private float killPowerDuration;
    [SerializeField] private float collectPowerDuration;
    private ParticleSystem skullParticle;

    private GameObject playerObject;
    private Collector itemCollector;


    [SerializeField] BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        skullParticle = GetComponentInChildren<ParticleSystem>();
        col.enabled = false;
    }

    private void Start()
    {

        playerObject = GameObject.FindGameObjectWithTag("Player");
        itemCollector = playerObject.GetComponent<Collector>();

        SpawnPower(_KillAll);
        SpawnPower(_CollectAll);
    }
    public void SpawnPower(PowerClass powerClass)
    {
       GameObject ActivePower= Instantiate(powerClass._PowerPrefab, new Vector2(GetRandomInt(SpawnRangeMinX,SpawnRangeMaxX), 9), Quaternion.identity, null);
        ActivePower.transform.DOMoveY(GetRandomInt(SpawnRangeMinY, SpawnRangeMaxY), 3f).OnComplete(()=> {
            ActivePower.GetComponent<SpriteRenderer>().sprite = powerClass._afterDrop;
            StartCoroutine(VanishPower(ActivePower.GetComponent<SpriteRenderer>()));
        });
    }
    public int GetRandomInt(int Min,int Max)
    {
        return Random.Range(Min, Max);
    }
    IEnumerator VanishPower(SpriteRenderer spriteRenderer)
    {
        
        yield return new WaitForSeconds(waitTimeToVansish);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(spriteRenderer.DOFade(0.5f, 0.75f));
        sequence.Append(spriteRenderer.DOFade(1f, 0.75f));
        sequence.Append(spriteRenderer.DOFade(0.5f, 0.75f));
        sequence.Append(spriteRenderer.DOFade(1f, 0.75f));
        sequence.Append(spriteRenderer.DOFade(0.5f, 1.5f).OnComplete(()=> {
            Destroy(spriteRenderer.gameObject);
         
        }));
    }
    public void KillAllEnemies()
    {
        col.enabled = true;
        //Debug.Log("Killing all");
        Invoke(nameof(DisableCollider), killPowerDuration);
    }
    public void CollectAll()
    {
        col.enabled = true;
        //Debug.Log("Collecting all");
        Invoke(nameof(DisableCollider), collectPowerDuration);
    }


    private void DisableCollider()
    {
        col.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("Inside the collider");
        switch (other.tag)
        {
            case "Collectable":
                var collectable = other.transform.GetComponent<Collectable>();
                itemCollector.CollectItem(collectable);
                //Destroy();
                break;


            case "Enemy":
                Debug.Log("Taking damage to the player");
                var enemyHealth = other.transform.GetComponent<EnemyEntity>();
                enemyHealth.TakeDamage((int)enemyHealth.CurrentHealth);
                break;


            default:
                break;
        }
    }
}
