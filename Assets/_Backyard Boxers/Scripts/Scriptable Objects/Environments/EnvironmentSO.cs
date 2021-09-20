using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Environment")]
public class EnvironmentSO : ScriptableObject {

    public int uniqueID;
    public Sprite environmentSprite;
    public string environmentName;
    public string sceneName;
}