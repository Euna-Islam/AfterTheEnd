using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    private static OxygenManager instance;
    public static OxygenManager Instance { get { return instance; } }

    public float OxygenLossRate = 0.0005f;
    public float OxygenGainRate = 0.0005f;
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
        
        if (PlayerMovement.Instance.IsInPollutedArea)
            OxygenLevel.fillAmount -= OxygenLossRate;
        else OxygenLevel.fillAmount += OxygenGainRate;

        if (GameManager.Instance.IsGamePlaying() && OxygenLevel.fillAmount < .1)
        {
            GameManager.Instance.GameOver();
        }

    }

}
