using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int collectScore = 10;

    private ObjectPooler pooler;


    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }
    public int CollectScore
    {
        get { return collectScore; }
        private set { collectScore = value; }
    }

    public void Collect()
    {
        pooler.ReturnToPool(gameObject);
    }
}
