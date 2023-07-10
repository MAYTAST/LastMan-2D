using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int collectScore = 10;

    public int CollectScore
    {
        get { return collectScore; }
        private set { collectScore = value; }
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}
