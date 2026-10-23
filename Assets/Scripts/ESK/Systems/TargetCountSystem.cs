using FFS.Libraries.StaticEcs;

public struct TargetCountSystem : ISystem
{
    public void Update()
    {
        
        int startGameNumber = GameWorld.Query<All<GameStarted>>().EntitiesCount();
            if (startGameNumber == 0)
            {
                
               return;
            
            }   
        int targetCount =3;
        int count = GameWorld.Query<All<Target>>().EntitiesCount();
        int requestCount = 0;
        GameWorld.Query().For(
            (in SpawnRequest request) =>
            {
                if (request.ObjectType == SpawnObjectType.Target)
                {
                    requestCount++;
                }
            });  
        int missingTargets = targetCount - requestCount - count;
        for (int i = 0;i < missingTargets; i++)
        {
            var entity = GameWorld.NewEntity<Default>();
            entity.Set(new SpawnRequest{
                ObjectType = SpawnObjectType.Target,
                ConfigID = -1
            });
        }
         
    }
}