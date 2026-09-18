using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetMoveSystem : ISystem
{
    public void Update()
    {
        GameWorld.Query().For(
            static (ref Position position) =>
            {
                position.Value += Vector2.right * Time.deltaTime;
            });
    }
}