using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image OxygenLevel;

    public float PollutedAreaHeight;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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
    private void Update()
    {
        UpdateOxygenIndicator();
    }

    void UpdateOxygenIndicator() {
        float oxygenLossRate = 0.0005f;
        float oxygenGainRate = 0.0005f;
        if (PlayerMovement.Instance.IsInPollutedArea)
            OxygenLevel.fillAmount -= oxygenLossRate;
        else OxygenLevel.fillAmount += oxygenGainRate;
    }
}
