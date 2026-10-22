using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetSpawnSystem : ISystem
{
    private TargetCatalog targetCatalog;
    public TargetSpawnSystem(TargetCatalog targetCatalog)
        {
            this.targetCatalog = targetCatalog;
        }
    public void Spawn(GameWorld.Entity targetEntity)
        {
            TargetConfig config = targetCatalog.Targets[Random.Range(0, targetCatalog.Targets.Length)];
            targetEntity.Set(new Target());
            targetEntity.Set(new NeedsSpawn());
            targetEntity.Set(new Position
                {
                    Value = new Vector2(5, 3)
                });
                targetEntity.Set(new TargetRadius
                {
                    Value = config.Radius
                });
                targetEntity.Set(new TargetPrefab
                {
                    Value = config.Prefab
                });
        }
    public void Update()
    {
        int startGameNumber = GameWorld.Query<All<GameStarted>>().EntitiesCount();
            if (startGameNumber == 0)
            {
                
               return;
            
            };
    var spawnSystem = this;
    GameWorld.Query().For(
    (GameWorld.Entity entity, in SpawnRequest request) =>
    {
        spawnSystem.Spawn(entity);
    });
    GameWorld.Query<All<SpawnRequest>>().BatchDelete<SpawnRequest>();
    }
}