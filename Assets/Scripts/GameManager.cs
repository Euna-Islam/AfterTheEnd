using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public float PollutedAreaHeight;
    public float ReducePollutionSpeed;

    public GameObject MaleFlowerPollen, FemaleFlowerPollen;
    public GameObject PollutedSky, CleanSky, BottomPollution;

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
        GetComponent<LevelManager>().UpdateLevel();
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
        MaleFlowerPollen.SetActive(true);
        FemaleFlowerPollen.SetActive(false);
        SetPollutedSky();
    }

    void SetPollutedSky() {
        Color c = PollutedSky.GetComponent<SpriteRenderer>().color;
        PollutedSky.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);
        c = BottomPollution.GetComponent<SpriteRenderer>().color;
        BottomPollution.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, 1);
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
            c = BottomPollution.GetComponent<SpriteRenderer>().color;
            BottomPollution.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);
            //PollutedMountain.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);
        }    
    }

    public void IncreasePlayerLevel() {
        CurrentLevel++;
        if (CurrentLevel <= MaxLevel)
        {
            Replay();
        }  
        else GameOver();
    }

    public bool IsGamePlaying() {
        return GamePlayState == GameState.PLAY;
    }
}
