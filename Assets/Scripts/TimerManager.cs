using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text Timer;

    public int TotalTime = 10;

    private static TimerManager instance;
    public static TimerManager Instance { get { return instance; } }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        Timer.text = TotalTime + "s";
    }

    public void StartTimer() {
        InvokeRepeating("ReduceTime", 1f, 1f);
    }

    void ReduceTime() {
        TotalTime -= 1;
        Timer.text = TotalTime + "s";

        if (TotalTime <= 0)
            GameManager.Instance.GameOver();
    }
}
