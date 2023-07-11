using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static Action<float> OnCollect;
    private float currentScore = 0;



    private void Start()
    {
        currentScore = 0;
        OnCollect?.Invoke(currentScore);    
    }

    public void CollectItem(Collectable itemToCollect)
    {

        currentScore += itemToCollect.CollectScore;
        OnCollect?.Invoke(itemToCollect.CollectScore);
        itemToCollect.Collect();
    }

}
