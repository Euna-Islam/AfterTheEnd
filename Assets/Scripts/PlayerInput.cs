using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        TakePlayerInput();
    }

    void TakePlayerInput() {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        float x = Input.GetAxis("Horizontal");
        PlayerMovement.Instance.ChangeHorizontalDirection(x);

        if (Input.GetKey(KeyCode.Space))
            PlayerMovement.Instance.MovePlayerUp();
        //else PlayerMovement.Instance.ChangeVerticalDirection(false);
    }
}
