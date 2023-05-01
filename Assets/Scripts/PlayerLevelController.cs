using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelController : MonoBehaviour
{
    private static PlayerLevelController instance;
    public static PlayerLevelController Instance { get { return instance; } }

    public int CurrentLevel;
    public int MaxLevel;

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
}
