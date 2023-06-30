using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable: ScriptableObject
{
    public bool value;

    public void FlipBool()
    {
        value = !value;
    }
}
