using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetSpawnSystem : ISystem
{
        private TargetCatalog targetCatalog;

        public TargetSpawnSystem(TargetCatalog targetCatalog)
        {
            this.targetCatalog = targetCatalog;
        }
    public void Spawn(GameWorld.Entity targetEntity, int configID)
        {
            if (configID == -1)
            {
                configID = Random.Range(0, targetCatalog.Targets.Length);
            }

            if (configID < 0 || configID >= targetCatalog.Targets.Length)
            {
                Debug.LogError($"Невозможно создать мишень: неверный ConfigID = {configID}.");
                return;
            }
            TargetConfig config = targetCatalog.Targets[configID];
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
        if (request.ObjectType == SpawnObjectType.Target)
            {
                spawnSystem.Spawn(entity, request.ConfigID);
                entity.Set(new SpawnRequestProcessed());
            }
        else if (request.ObjectType == SpawnObjectType.Prefab)
        {
            if (request.Prefab == null)
            {
                Debug.LogError("SpawnRequest: не указан префаб.");
            }
            else
            {
                entity.Set(new Position
                {
                    Value = request.Position
                });

                entity.Set(new TargetPrefab
                {
                    Value = request.Prefab
                });
            }
            entity.Set(new SpawnRequestProcessed());
        }
    });
    GameWorld.Query<All<SpawnRequestProcessed>>().BatchDelete<SpawnRequest>();
    GameWorld.Query<All<SpawnRequestProcessed>>().BatchDelete<SpawnRequestProcessed>();
    }
}