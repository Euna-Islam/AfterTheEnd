using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text Level;

    public void UpdateLevel() {
        Level.text = "Level " + PlayerLevelController.Instance.CurrentLevel;
    }
}
