using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct CrosshairViewSystem : ISystem
{
    private GameObject crosshair;

    public CrosshairViewSystem(GameObject crosshair)
        {
    this.crosshair = crosshair;
        }
    public void Update()
    {
        GameObject currentCrosshair = crosshair;    
            GameWorld.Query().For(
            (in Position position, in Crosshair crosshair) =>
            {
                currentCrosshair.transform.position = position.Value;
            });
    }
}