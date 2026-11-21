using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Keeps the result after Level02 is unloaded (DontDestroyOnLoad).</summary>
public sealed class GameSession : MonoBehaviour
{
    private static GameSession instance;
    public static GameSession Instance
    {
        get
        {
            if (instance == null)
            {
                var go = new GameObject("GameSession_Persistent");
                instance = go.AddComponent<GameSession>();
            }
            return instance;
        }
    }

    public int crystalsCollected { get; private set; }
    public int totalAttempts { get; private set; }
    public float remainingEnergy { get; private set; }
    public float elapsedSeconds { get; private set; }
    public bool completed { get; private set; }
    private float levelStartTime;
    private bool transitioning;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BeginLevel()
    {
        totalAttempts++;
        crystalsCollected = 0;
        remainingEnergy = 60f;
        elapsedSeconds = 0f;
        completed = false;
        transitioning = false;
        levelStartTime = Time.time;
        MobileInputState.Clear();
    }

    public void AddCrystal() { crystalsCollected++; }

    public void FinishLevel(float energy)
    {
        if (transitioning) return;
        transitioning = true;
        completed = true;
        remainingEnergy = Mathf.Max(0f, energy);
        elapsedSeconds = Mathf.Max(0f, Time.time - levelStartTime);
        SceneManager.LoadScene("Victory");
    }

    public void FailLevel(float energy)
    {
        if (transitioning) return;
        transitioning = true;
        completed = false;
        remainingEnergy = Mathf.Max(0f, energy);
        elapsedSeconds = Mathf.Max(0f, Time.time - levelStartTime);
        SceneManager.LoadScene("GameOver");
    }
}
