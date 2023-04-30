using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float PollutedAreaHeight;
    public float ReducePollutionSpeed;

    public GameObject MaleFlowerPollen, FemaleFlowerPollen;
    public GameObject Forest1, Forest2, Forest3;
    public GameObject PollutedSky, CleanSky;

    public GameObject GameStartPanel, GamePanel, GameOverPanel;

    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    bool CleaningSky;
    public enum GameState
    {
        GAMEOVER,
        PLAY,
        START
    }

    public GameState GamePlayState;

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

    private void Start()
    {
        Play();
    }

    private void Update()
    {
        if (CleaningSky)
            FadePollutedSky();
    }

    public void GenerateCleanWorld()
    {
        PollutionGenerator.Instance.StopPollutionGeneration();
        CleaningSky = true;

        //StartCoroutine(GenerateFirstPart());

    }

    IEnumerator GenerateFirstPart() {
        yield return new WaitForSeconds(1);
        SetCleanSky();
        //Forest1.SetActive(true);
        //StartCoroutine(GrowSecondPart());
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
        CleaningSky = false;
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
        SetPollutedSky();
    }

    void SetPollutedSky() {
        Color c = PollutedSky.GetComponent<SpriteRenderer>().color;
        PollutedSky.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);

        //PollutedMountain.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);
        //PollutedSky.SetActive(true);
        //CleanSky.SetActive(false);
    }

    void SetCleanSky()
    {
        InvokeRepeating("FadePollutedSky", .1f, 1);
    }

    void FadePollutedSky() {
        Color c = PollutedSky.GetComponent<SpriteRenderer>().color;
        if (c.a <= 0) {
            CleaningSky = false;
            //GameOver();
        } else
        {
            float alpha = ReducePollutionSpeed * Time.deltaTime;
            PollutedSky.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);
            //PollutedMountain.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);
        }    
    }

    public void IncreasePlayerLevel() {
        if (CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            Replay();
        }  
        else GameOver();
    }

    public bool IsGamePlaying() {
        return GamePlayState == GameState.PLAY;
    }
}
