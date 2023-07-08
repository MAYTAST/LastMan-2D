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



    private void Start()
    {
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
      
    }
    public void CollectAll()
    {
        
    }
}
