using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    int NarrationCount = 0;
    public List<GameObject> Tips;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        NarrationCount = 0;
        InvokeRepeating("ShowTips", 1, 2);    
    }

    void ShowTips() {
        for (int i = 0; i < Tips.Count; i++) {
            if (i == NarrationCount)
                Tips[i].SetActive(true);
            else Tips[i].SetActive(false);
        }
        NarrationCount++;
        if (NarrationCount == Tips.Count)
            NarrationCount = 0;
    }
}
