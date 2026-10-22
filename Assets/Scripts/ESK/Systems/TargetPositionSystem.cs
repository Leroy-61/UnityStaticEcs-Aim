using FFS.Libraries.StaticEcs;
using UnityEngine;

public struct TargetPositionSystem : ISystem
{
    public void Update()
    {
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

    }
}