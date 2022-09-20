using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int Score { get => score; }

    public static CoinManager Instance { get => instance; }

    [SerializeField] UnityEvent onUpdate;

    static CoinManager instance;

    Text txt;
    int score = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        
    }

    public void Increment()
    {
        score++;
        txt.text = $"Score: {score}";
        onUpdate.Invoke();
    }
}
