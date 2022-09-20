using UnityEngine;

public class ScoreIncrement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        CoinManager.Instance.Increment();
        Destroy(gameObject);
    }

}
