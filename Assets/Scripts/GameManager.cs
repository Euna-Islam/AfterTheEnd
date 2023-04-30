using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float PollutedAreaHeight;

    public GameObject CarbonDiOxide, MaleFlower, FemaleFlower;
    public GameObject Forest1, Forest2, Forest3;

    public GameObject GameStartPanel, GamePanel, GameOverPanel;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public enum GameState
    {
        GAMEOVER,
        PLAY,
        START
    }

    public GameState GamePlayState;

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

    private void Start()
    {
        GamePlayState = GameState.START;
    }
    
    public void GrowForest()
    {
        GrowFirstPart();
        StartCoroutine(GrowFirstPart());
        
    }

    IEnumerator GrowFirstPart() {
        yield return new WaitForSeconds(1);
        Forest1.SetActive(true);
        StartCoroutine(GrowSecondPart());
    }

    IEnumerator GrowSecondPart()
    {
        yield return new WaitForSeconds(1);
        Forest2.SetActive(true);
        StartCoroutine(GrowThirdPart());
    }

    IEnumerator GrowThirdPart()
    {
        yield return new WaitForSeconds(1);
        Forest3.SetActive(true);
        CarbonDiOxide.SetActive(false);

        StartCoroutine(EndGame());
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(1);
        GameOver();
        StopAllCoroutines();
    }


    public void Play()
    {
        PlayerMovement.Instance.Reset();
        ResetGame();
        GamePlayState = GameState.PLAY;
        GameStartPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GamePanel.SetActive(true);
        OxygenManager.Instance.Reset();
        TimerManager.Instance.StartTimer();
    }

    public void Replay()
    {
        Play();
    }

    public void GameOver()
    {
        TimerManager.Instance.Reset();
        GamePlayState = GameState.GAMEOVER;
        GamePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }

    public void ResetGame() {
        Forest1.SetActive(false);
        Forest2.SetActive(false);
        Forest3.SetActive(false);
        CarbonDiOxide.SetActive(true);
        MaleFlower.SetActive(true);
        FemaleFlower.SetActive(true);
    }

    public bool IsGamePlaying() {
        return GamePlayState == GameState.PLAY;
    }
}
