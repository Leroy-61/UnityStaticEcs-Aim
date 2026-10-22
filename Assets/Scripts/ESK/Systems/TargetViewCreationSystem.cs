using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetViewCreationSystem : ISystem
{
    private TargetPool pool;

    public TargetViewCreationSystem(TargetPool pool)
    {
        this.pool = pool;
    }

    public void Update()
    {
        TargetPool currentPool = pool;

        GameWorld.Query().For(
            (GameWorld.Entity entity, in TargetPrefab prefab, in Position position) =>
            {
                if (entity.Has<TargetView>() || entity.Has<NeedsSpawn>())
                {
                    return;
                }

                GameObject newTarget = currentPool.Get(prefab.Value);

                newTarget.transform.position = new Vector3(
                    position.Value.x,
                    position.Value.y,
                    0f
                );

                entity.Set(new TargetView
                {
                    Value = newTarget
                });
            });
    }
}