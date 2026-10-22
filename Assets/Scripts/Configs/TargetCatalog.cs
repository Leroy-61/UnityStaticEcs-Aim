using UnityEngine;

[CreateAssetMenu(fileName = "TargetCatalog", menuName = "Game/Target Catalog")]
public class TargetCatalog : ScriptableObject
{
    public TargetConfig[] Targets;
}