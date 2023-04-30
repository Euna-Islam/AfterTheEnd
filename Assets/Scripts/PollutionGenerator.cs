using System.Collections.Generic;
using UnityEngine;

public class PollutionGenerator : MonoBehaviour
{
    public GameObject PollutionBubble;

    public float BubbleMinRangeX;
    public float BubbleMaxRangeX;

    List<GameObject> AllBubbles;

    private void Start()
    {
        AllBubbles = new List<GameObject>();
        GenerateBubble();
    }

    void GenerateBubble() {
        int noOfBubbles = Random.Range(2, 4);

        for (int i = 0; i < AllBubbles.Count; i++) {
            Destroy(AllBubbles[i]);
        }


        AllBubbles.Clear();
        for (int i = 0; i < noOfBubbles; i++)
        {
            float positionX = Random.Range(BubbleMinRangeX, BubbleMaxRangeX);
            Debug.Log(positionX);
            Vector3 position = new Vector3(positionX, PollutionBubble.transform.position.y, PollutionBubble.transform.position.z);
            GameObject bubble = Instantiate(PollutionBubble);
            bubble.transform.position = position;
            bubble.SetActive(true);
            AllBubbles.Add(bubble);
        }
    }
}
