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

    /// <summary>
    /// to be removed lated
    /// </summary>
    public SpriteRenderer PlayerSprite;

    public bool IsInPollutedArea, IsResting;

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
        PlayerHorizontalDirection = Direction.RIGHT;
        PlayerVerticalDirection = Direction.DOWN;
        IsInPollutedArea = false;
        IsResting = false;
    }

    private void LateUpdate()
    {
        FlipPlayer();
        //if(!IsResting)
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
        float nextPosY = PlayerVerticalDirection == Direction.DOWN ?
            currentPos.y - DownwardDisplacement : currentPos.y + UpwardDisplacement;
        Vector2 nextPos = new Vector3(nextPosX, nextPosY);

        transform.position = Vector3.Lerp(currentPos, nextPos, Speed * Time.deltaTime);
    }

    void HasEnteredPollutedArea() {

        if (transform.position.y <= GameManager.Instance.PollutedAreaHeight)
            IsInPollutedArea = true;
        else IsInPollutedArea = false;
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
        PlayerVerticalDirection = isGoingUp ? Direction.UP : Direction.DOWN;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Ground") {
    //        IsResting = true;
    //    }
    //}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.transform.tag == "Ground")
    //    {
    //        IsResting = false;
    //    }
    //}


}
