using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidCloudController : MonoBehaviour
{
    public GameObject RainDrop;
    public GameObject DropAnim;

    public float CloudSpeed;
    public float MinX, MaxX;

    public List<GameObject> RainDrops;

    public float RainMaxX, RainMinX;

    bool isGoingRight;

    private void Start()
    {
        InvokeRepeating("GenerateRain", 0.1f, 0.1f);
        isGoingRight = false;
    }

    private void Update()
    {
        if (PlayerMovement.Instance.IsPolinated()) {
            gameObject.SetActive(false);
        } else
            Move();
        //GenerateRain();
    }

    public void Move()
    {
        Vector2 newPos;
        if(isGoingRight)
            newPos = new Vector2(MaxX, transform.position.y);
        else newPos = new Vector2(MinX, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPos, CloudSpeed * Time.deltaTime);
        if (transform.position.x == MinX)
            isGoingRight = true;
        else if (transform.position.x == MaxX)  
            isGoingRight = false;
    }

    void GenerateRain() {
        if (RainDrops.Count == 0)
        {
            CancelInvoke("GenerateRain");
            return;
        }
        int randomIndex = Random.Range(0, RainDrops.Count-1);
        
        GameObject rain = RainDrops[randomIndex];
        float x = Random.Range(transform.position.x - 1.5f, transform.position.x + 1.5f);
        rain.transform.position = new Vector2(x, rain.transform.position.y);
        rain.SetActive(true);
        RainDrops.Remove(rain);
    }


}
