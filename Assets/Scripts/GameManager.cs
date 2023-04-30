using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float PollutedAreaHeight;

    public GameObject MaleFlowerPollen, FemaleFlowerPollen;
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
        Play();
    }
    
    public void GrowForest()
    {
        PollutionGenerator.Instance.StopPollutionGeneration();
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
        GameOverPanel.SetActive(false);
        GamePanel.SetActive(true);
        OxygenManager.Instance.Reset();
        TimerManager.Instance.StartTimer();
        PollutionGenerator.Instance.GeneratePollution();
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
        MaleFlowerPollen.SetActive(true);
        FemaleFlowerPollen.SetActive(false);
    }

    public bool IsGamePlaying() {
        return GamePlayState == GameState.PLAY;
    }
}
