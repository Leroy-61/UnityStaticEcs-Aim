using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetSpawnSystem : ISystem
{
    private GameObject targetView;
    public TargetSpawnSystem(GameObject targetView)
        {
            this.targetView = targetView;
        }
    public void Spawn()
        {
            var targetEntity = GameWorld.NewEntity<Default>();
            targetEntity.Set(new Target());
            targetEntity.Set(new NeedsSpawn());
            targetEntity.Set(new Position
                {
                    Value = new Vector2(3, 5)
                });
            targetEntity.Set(new TargetView
                {
                    Value = targetView
                });
        }
    public void Update()
        {
        }
}