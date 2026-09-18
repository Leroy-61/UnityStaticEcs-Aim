using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetHitSystem : ISystem
{
    private GameObject crosshair;
        public TargetHitSystem(GameObject crosshair)
        {
            this.crosshair = crosshair;
        }   
    public void Update()
    {
        var currentCrosshair = crosshair;
        GameWorld.Query().For(
        (in Target target, in Position position) =>
        {
            Debug.Log($"Crosshair: {currentCrosshair.transform.position}, Target: {position.Value}");
        });
    }
}