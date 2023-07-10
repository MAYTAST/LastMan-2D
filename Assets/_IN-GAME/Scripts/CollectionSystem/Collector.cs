using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public static Action<int> OnCollect;
    private int currentScore = 0;
    public void CollectItem(Collectable itemToCollect)
    {

        currentScore += itemToCollect.CollectScore;
        OnCollect?.Invoke(currentScore);
        itemToCollect.Collect();
    }

}
