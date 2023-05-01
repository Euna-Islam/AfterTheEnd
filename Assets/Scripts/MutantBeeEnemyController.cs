using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantBeeEnemyController : MonoBehaviour
{
    public float Speed;
    public GameObject Player;
    public SpriteRenderer sprite;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (PlayerMovement.Instance.IsPolinated())
            gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        Vector2 newPos = Player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, newPos, Speed * Time.deltaTime);

        if(transform.position.x < newPos.x)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}
