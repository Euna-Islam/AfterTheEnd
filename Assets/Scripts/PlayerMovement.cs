using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement Instance { get { return instance; } }

    [SerializeField]
    private float HorizontalDisplacement;
    [SerializeField]
    private float UpwardDisplacement;
    [SerializeField]
    private float DownwardDisplacement;
    [SerializeField]
    private float Speed;

    public enum Direction
    {
        LEFT,
        RIGHT,
        STILL,
        UP,
        DOWN
    }

    public Direction PlayerHorizontalDirection;
    public Direction PlayerVerticalDirection;

    public SpriteRenderer PlayerSprite;

    public bool IsInPollutedArea, IsResting;

    Rigidbody2D rb;

    public enum PollenState
    {
        COLLECTED,
        DELIVERED,
        NOT_COLLECTED
    }

    public PollenState PollenCollectionState;

    Vector3 InitialPosition;

    public Animator BeeAnim;

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

        InitialPosition = transform.position;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Reset();
    }

    public void Reset()
    {
        PlayerHorizontalDirection = Direction.RIGHT;
        PlayerVerticalDirection = Direction.DOWN;
        IsInPollutedArea = false;
        IsResting = true;
        PollenCollectionState = PollenState.NOT_COLLECTED;
        transform.position = InitialPosition;
    }

    private void Update()
    {
        FlipPlayer();
        MovePlayer();
        HasEnteredPollutedArea();
    }

    void MovePlayer()
    {
        Vector2 currentPos = transform.position;
        float nextPosX = 0;
        switch (PlayerHorizontalDirection)
        {
            case Direction.RIGHT:
                nextPosX = currentPos.x + HorizontalDisplacement;
                break;
            case Direction.LEFT:
                nextPosX = currentPos.x - HorizontalDisplacement;
                break;
            default:
                nextPosX = currentPos.x;
                break;
        }

        float nextPosY = currentPos.y;
        // Does the ray intersect any objects excluding the player layer
        if (IsResting)
        {
            nextPosY = currentPos.y;
        }
        else {
            nextPosY = PlayerVerticalDirection == Direction.DOWN ? currentPos.y - DownwardDisplacement :
                (IsInPollutedArea ? currentPos.y + UpwardDisplacement / 2 : currentPos.y + UpwardDisplacement);
        }
        Vector2 nextPos = new Vector3(nextPosX, nextPosY);

        transform.position = Vector3.Lerp(currentPos, nextPos, Speed * Time.deltaTime);
    }

    public void MovePlayerUp() {
        //float force = IsInPollutedArea ? UpwardDisplacement / 2 : UpwardDisplacement;
        //rb.AddForce(Vector2.up * force);
        Vector2 currentPos = transform.position;
        float nextPosY = (IsInPollutedArea ? currentPos.y * UpwardDisplacement / 2 : currentPos.y * UpwardDisplacement);

        Vector2 nextPos = new Vector3(currentPos.x, nextPosY);

        transform.position = Vector3.Lerp(currentPos, nextPos, Speed * Time.deltaTime);
    }

    void HasEnteredPollutedArea() {

        if (transform.position.y <= GameManager.Instance.PollutedAreaHeight && !IsPolinated())
        {
            IsInPollutedArea = true;
            BeeAnim.SetBool("IsBeeHurt", true);
        }
        else {
            IsInPollutedArea = false;
            BeeAnim.SetBool("IsBeeHurt", false);
        }
    }


    void FlipPlayer() {
        switch (PlayerHorizontalDirection)
        {
            case Direction.RIGHT:
                PlayerSprite.flipX = false;
                break;
            case Direction.LEFT:
                PlayerSprite.flipX = true;
                break;
        }
        
    }

    public void ChangeHorizontalDirection(float horizontalAxis)
    {
        PlayerHorizontalDirection = horizontalAxis == 0 ? Direction.STILL :
                                    horizontalAxis > 0 ? Direction.RIGHT : Direction.LEFT;
    }

    public void ChangeVerticalDirection(bool isGoingUp)
    {
        //if (isGoingUp)
        //    MovePlayerUp();
        //else rb.gravityScale = .5f;
        PlayerVerticalDirection = isGoingUp ? Direction.UP : Direction.DOWN;
        IsResting = isGoingUp ? false : IsResting;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground" || collision.transform.tag == "MaleFlower"
            || collision.transform.tag == "FemaleFlower" || collision.transform.tag == "Hive"
            )
        {
            Debug.Log("Hitting ground");
            IsResting = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        if (!IsPolinated() && (collision.transform.tag == "RainUnit" || collision.transform.tag == "Mutant"))
        {
            ActivateDeadAnim();
            GameManager.Instance.GameOver();
            //SoundEffectManager.Instance.PlayEffect(0);
        }


        if (collision.transform.tag == "MaleFlower")
        {
            PollenCollectionState = PollenState.COLLECTED;
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //SoundEffectManager.Instance.PlayEffect(1);
        }

        if (collision.transform.tag == "FemaleFlower" && PollenCollectionState == PollenState.COLLECTED)
        {
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            PollenCollectionState = PollenState.DELIVERED;
            if (PollenCollectionState == PollenState.DELIVERED &&
                            GameManager.Instance.IsGamePlaying())
                GameManager.Instance.GenerateCleanWorld();
            IsInPollutedArea = false;
            //SoundEffectManager.Instance.PlayEffect(2);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Finish" && GameManager.Instance.IsGamePlaying()
            && IsPolinated())
        {
            gameObject.SetActive(false);
            GameManager.Instance.IncreasePlayerLevel();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsResting = false;
    }

    public bool IsPolinated()
    {
        return PollenCollectionState == PollenState.DELIVERED;
    }

    public void ActivateDeadAnim() {
        Debug.Log("dead");
        BeeAnim.SetBool("IsDead", true);
    }

}
