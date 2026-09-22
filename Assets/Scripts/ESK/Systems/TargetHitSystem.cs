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
        var currentCrosshair = crosshair;
        if (!Mouse.current.leftButton.wasPressedThisFrame)
        {
        return;
        }
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
    GameWorld.Query<All<NeedsDestroy>>().BatchDestroy();
    var spawnSystem = new TargetSpawnSystem(targetView);
        spawnSystem.Spawn();
    }
    }
}