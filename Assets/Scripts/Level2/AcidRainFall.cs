using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidRainFall : MonoBehaviour
{
    public GameObject RainDrop;
    public GameObject DropAnim;

    Vector2 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RainDrop.SetActive(false);
        DropAnim.SetActive(true);
        Invoke("Reset", 0.5f);
    }

    private void Reset()
    {
        float x = Random.Range(transform.parent.position.x - 1.5f, transform.parent.position.x + 1.5f);
        transform.position = new Vector2(x, InitialPos.y);
        RainDrop.SetActive(true);
        DropAnim.SetActive(false);
    }


}
