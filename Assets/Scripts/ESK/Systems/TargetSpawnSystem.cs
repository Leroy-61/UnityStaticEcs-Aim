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
            Debug.Log("Мишень 5,3 спавн");
            var targetEntity = GameWorld.NewEntity<Default>();
            targetEntity.Set(new Target());
            targetEntity.Set(new NeedsSpawn());
            targetEntity.Set(new Position
                {
                    Value = new Vector2(5, 3)
                });
                targetEntity.Set(new TargetRadius
                {
                    Value = 0.5f
                });
            GameObject newTarget = Object.Instantiate(targetView);

            targetEntity.Set(new TargetView
            {
                Value = newTarget
            });
        }
    public void Update()
    {
        int startGameNumber = GameWorld.Query<All<GameStarted>>().EntitiesCount();
            if (startGameNumber == 0)
            {
                
               return;
            
            };
        GameWorld.Query().For((GameWorld.Entity entity, ref Position position, in NeedsSpawn needsSpawn) =>
                {
                    Vector2 candidatePosition = new Vector2(0,0);
            bool positionFound = false;
            while (!positionFound)
                    {
                    positionFound = true;
                    candidatePosition = new Vector2(
                        Random.Range(-5f, 5f),
                        Random.Range(-3f, 3f));

                    GameWorld.Query<All<Target>>().For(
                        (GameWorld.Entity targetEntity, in Position foundposition, in TargetRadius foundTargetRadius, in Target foundtarget)=>
                        {  
                            if (targetEntity == entity)
                                {
                                    return;
                                }
                            float distanceSquared =  (candidatePosition - foundposition.Value).sqrMagnitude;
                            float minDistanceSquared = ((2.5f*(foundTargetRadius.Value))*(2.5f*(foundTargetRadius.Value)));
                            if(distanceSquared < minDistanceSquared)
                                {
                                    positionFound = false;
                                }
                    }
                );
            }; 
        position.Value = candidatePosition;
        entity.Delete<NeedsSpawn>();
        }
        );
        int targetCount =3;
        int count = GameWorld.Query<All<Target>>().EntitiesCount();
        for (int i = 0; i < (targetCount - count); i++)
        {
            Spawn();
        }
    }
}