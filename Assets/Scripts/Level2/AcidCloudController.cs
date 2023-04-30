using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidCloudController : MonoBehaviour
{
    public GameObject RainDrop;
    public GameObject DropAnim;

    public float CloudSpeed;
    public float MinX, MaxX;

    public float RainMaxX, RainMinX;

    bool isGoingRight;

    private void Start()
    {
        isGoingRight = false;
    }

    private void Update()
    {
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
        float rainXPos = Random.Range(RainMinX, RainMaxX);
        Vector2 newPos = new Vector2(rainXPos, RainDrop.transform.position.y);
        GameObject rain = Instantiate(RainDrop, newPos, RainDrop.transform.rotation);
        rain.transform.SetParent(transform);
    }


}
