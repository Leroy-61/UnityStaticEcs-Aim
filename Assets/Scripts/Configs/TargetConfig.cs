using UnityEngine;

[CreateAssetMenu(fileName = "TargetConfig", menuName = "Game/Target Config")]
public class TargetConfig : ScriptableObject
{
    public GameObject Prefab;
    public float Radius;
}