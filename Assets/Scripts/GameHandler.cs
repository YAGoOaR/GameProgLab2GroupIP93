using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] string nextSceneName = "Level2";

    public static GameHandler Instance { get => instance; }

    static GameHandler instance;

    private void Start()
    {
        instance = this;
    }

    public void NextLevel()
    {
        if (CoinManager.Instance.Score >= 5)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
