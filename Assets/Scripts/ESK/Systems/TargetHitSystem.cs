using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetHitSystem : ISystem
{
    public void Update()
    {
        bool hasShoot = false;
        GameWorld.Query().For(
        (GameWorld.Entity entity, in ShootEvent shootEvent) =>
        {
            hasShoot = true;
        });
        if (!hasShoot)
        {
            return;
        }
            Vector2 crosshairPosition = Vector2.zero;
            GameWorld.Query().For(
            (in Position position, in Crosshair crosshair)=>
            {
                crosshairPosition = position.Value;
            });
        bool hit = false;
        GameWorld.Query().For(
        (GameWorld.Entity entity, in Target target, in Position position, in TargetRadius radius) =>
        {
            Vector2 targetPosition = position.Value;
            float distance = Vector2.Distance(crosshairPosition, targetPosition);
            Debug.Log($"Distance: {distance}, Radius: {radius.Value}");
            if (distance <= radius.Value)
                {
            Debug.Log("HIT!");
            entity.Set(new NeedsDestroy());
            hit = true;
                }
        });
    if (hit)
    {
        GameWorld.Query().For(
            static (in NeedsDestroy needsDestroy, in TargetView targetView)=>
            {
                UnityEngine.Object.Destroy(targetView.Value);
            });
            GameWorld.Query<All<NeedsDestroy>>().BatchDestroy();
    }
    GameWorld.Query<All<ShootEvent>>().BatchDelete<ShootEvent>();
    }
}