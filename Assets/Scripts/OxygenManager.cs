using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    private static OxygenManager instance;
    public static OxygenManager Instance { get { return instance; } }
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
    public Image OxygenLevel;
    private void Update()
    {
        UpdateOxygenIndicator();
    }

    public void Reset()
    {
        OxygenLevel.fillAmount = 1;
    }

    void UpdateOxygenIndicator()
    {
        float oxygenLossRate = 0.0005f;
        float oxygenGainRate = 0.0005f;
        if (PlayerMovement.Instance.IsInPollutedArea)
            OxygenLevel.fillAmount -= oxygenLossRate;
        else OxygenLevel.fillAmount += oxygenGainRate;

        if (GameManager.Instance.IsGamePlaying() && OxygenLevel.fillAmount < .1)
        {
            GameManager.Instance.GameOver();
        }

    }

}
