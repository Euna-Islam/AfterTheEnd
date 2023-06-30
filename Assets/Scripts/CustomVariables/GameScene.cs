using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scene/GameScene")]
public class GameScene : ScriptableObject
{
    public GameSceneType SceneType;

    public AssetReference SceneReference;

    public enum GameSceneType
    { 
        PersistantManagers,
        MainMenu,
        Level
    }
}
