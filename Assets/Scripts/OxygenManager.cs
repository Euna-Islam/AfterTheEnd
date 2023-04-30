using UnityEngine;
using UnityEngine.UI;

public class OxygenManager : MonoBehaviour
{
    private static OxygenManager instance;
    public static OxygenManager Instance { get { return instance; } }

    public float OxygenLossRate;
    public float OxygenGainRate;

    public Image OxygenLevel;
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

    public void Reset()
    {
        OxygenLevel.fillAmount = 1;
        CancelInvoke("UpdateOxygenIndicator");
        InvokeRepeating("UpdateOxygenIndicator", 0.5f, 0.5f);
    }

    void UpdateOxygenIndicator()
    {
        if (PlayerMovement.Instance.IsPolinated())
            return;
        if (PlayerMovement.Instance.IsInPollutedArea)
            OxygenLevel.fillAmount -= OxygenLossRate;
        //else OxygenLevel.fillAmount += OxygenGainRate;

        if (GameManager.Instance.IsGamePlaying() && OxygenLevel.fillAmount < .1)
        {
            GameManager.Instance.GameOver();
        }

    }

}
