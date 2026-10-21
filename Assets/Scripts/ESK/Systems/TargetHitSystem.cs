using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.InputSystem;

public struct TargetHitSystem : ISystem
{
    private GameObject crosshair;
    private GameObject targetView;
        public TargetHitSystem(GameObject crosshair,GameObject targetView)
        {
            this.crosshair = crosshair;
            this.targetView = targetView;
        }   
    public void Update()
    {
        bool hasShoot = true;
        var currentCrosshair = crosshair;
        GameWorld.Query().For(
        (GameWorld.Entity entity, in ShootEvent shootEvent) =>
        {
            bool hasShoot = true;
        });
        if (!hasShot)
        {
            return;
        };
        bool hit = false;
        GameWorld.Query().For(
        (GameWorld.Entity entity, in Target target, in Position position, in TargetRadius radius) =>
        {
            Vector2 crosshairPosition = currentCrosshair.transform.position;
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
    }
}