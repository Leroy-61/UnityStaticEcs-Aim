using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct SpawnRequest : IComponent
{
    public SpawnObjectType ObjectType;
    public int ConfigID;
    public GameObject Prefab;
    public Vector2 Position;
}