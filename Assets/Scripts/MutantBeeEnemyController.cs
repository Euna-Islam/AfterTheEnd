using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBeeEnemyController : MonoBehaviour
{
    public float Speed;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 newPos = Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);
    }
}
