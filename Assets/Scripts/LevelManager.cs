using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public TMP_Text Level;

    public void UpdateLevel() {
        Level.text = "Level " + GameManager.Instance.CurrentLevel;
    }
}
