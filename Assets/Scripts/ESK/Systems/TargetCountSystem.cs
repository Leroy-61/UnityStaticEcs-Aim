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
        int Requestcount = GameWorld.Query<All<SpawnRequest>>().EntitiesCount();   
        int missingTargets = targetCount - Requestcount - count;
        for (int i = 0;i < missingTargets; i++)
        {
            var entity = GameWorld.NewEntity<Default>();
            entity.Set(new SpawnRequest{
            ConfigID = 0
            });
        }
         
    }
}