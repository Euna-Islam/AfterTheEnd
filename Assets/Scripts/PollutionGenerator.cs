using System.Collections.Generic;
using UnityEngine;

public class PollutionGenerator : MonoBehaviour
{
    public GameObject PollutionBubble;

    public float BubbleMinRangeX;
    public float BubbleMaxRangeX;

    public List<GameObject> AllPollutions;

    int NextDeactivatePollution;

    private void Start()
    {
        GeneratePollution();
    }

    void GeneratePollution() {
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
