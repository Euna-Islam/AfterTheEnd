using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float PollutedAreaHeight;
    public float ReducePollutionSpeed;

    public GameObject MaleFlowerPollen, FemaleFlowerPollen;
    public GameObject PollutedSky, CleanSky, BottomPollution, BottomPollutionCloud;
    public GameObject MaleFlower, FemaleFlower, CleanGround, PollutedGround;
    public GameObject GameStartPanel, GamePanel, GameOverPanel, LevelCompletePanel, AllLevelsCompleterPanel;
    public GameObject ExitDoor;
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    bool CleaningSky;
    public enum GameState
    {
        GAMEOVER,
        PLAY,
        START,
        LEVEL_COMPLETE
    }

    public GameState GamePlayState;

    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        CleanGround.SetActive(false);
        Play();
    }

    private void Update()
    {
        if (CleaningSky)
        {
            FadePollutedSky();
            CleanGround.SetActive(true);
        }
            
    }

    public void GenerateCleanWorld()
    {
        PollutionGenerator.Instance.StopPollutionGeneration();
        CleaningSky = true;
        ExitDoor.SetActive(true);
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
        LevelCompletePanel.SetActive(false);
        GamePanel.SetActive(true);
        BottomPollutionCloud.SetActive(true);
        OxygenManager.Instance.Reset();
        //TimerManager.Instance.StartTimer();
        PollutionGenerator.Instance.GeneratePollution();
        GetComponent<LevelManager>().UpdateLevel();

    }

    public void Replay()
    {
        //Play();
        LoadNextScene();
    }

    public void GameOver()
    {
        //PlayerLevelController.Instance.CurrentLevel = 1;
        //TimerManager.Instance.Reset();
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
        if (c.a <= 0)
        {
            CleaningSky = false;
            //GameOver();
        }
        else
        {
            float alpha = ReducePollutionSpeed * Time.deltaTime;
            PollutedSky.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);
            c = BottomPollution.GetComponent<SpriteRenderer>().color;
            BottomPollution.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);

            c = PollutedGround.GetComponent<SpriteRenderer>().color;
            PollutedGround.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a -= alpha);

            //c = CleanGround.GetComponent<SpriteRenderer>().color;
            //CleanGround.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a += alpha);


            c = MaleFlower.GetComponent<SpriteRenderer>().color;
            MaleFlower.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a += alpha);
            c = FemaleFlower.GetComponent<SpriteRenderer>().color;
            FemaleFlower.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a += alpha);

            BottomPollutionCloud.SetActive(false);
        }
    }

    public void IncreasePlayerLevel() {
        PlayerLevelController.Instance.CurrentLevel++;
        GamePlayState = GameState.LEVEL_COMPLETE;
        GamePanel.SetActive(false);
        if (PlayerLevelController.Instance.CurrentLevel <= PlayerLevelController.Instance.MaxLevel)
        {
            LevelCompletePanel.SetActive(true);
        }  
        else DisplayAllLevelsCompletedPanel();
    }

    void DisplayAllLevelsCompletedPanel() {
        GamePanel.SetActive(false);
        AllLevelsCompleterPanel.SetActive(true);
    }



    public bool IsGamePlaying() {
        return GamePlayState == GameState.PLAY;
    }

    public void PlayFromBeginning()
    {
        PlayerLevelController.Instance.CurrentLevel = 1;
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(PlayerLevelController.Instance.CurrentLevel);
    }
}
