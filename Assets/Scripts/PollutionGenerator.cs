using System.Collections.Generic;
using UnityEngine;

public class PollutionGenerator : MonoBehaviour
{
    private static PollutionGenerator instance;
    public static PollutionGenerator Instance { get { return instance; } }

    //public GameObject PollutionBubble;

    public float BubbleMinRangeX;
    public float BubbleMaxRangeX;

    public List<GameObject> AllPollutions;

    int NextDeactivatePollution;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GeneratePollution();
    }

    public void StopPollutionGeneration()
    {
        CancelInvoke("ActivatePollution");
    }

    public void GeneratePollution() {
        CancelInvoke("ActivatePollution");
        InvokeRepeating("ActivatePollution", 1f, 1f);
    }

    void ActivatePollution() {
        int random = Random.Range(0, AllPollutions.Count - 1);
        AllPollutions[random].SetActive(true);
        NextDeactivatePollution = random;
        Invoke("DeactivatePollution", 2);
    }

    void DeactivatePollution() {
        AllPollutions[NextDeactivatePollution].SetActive(false);
    }
}
