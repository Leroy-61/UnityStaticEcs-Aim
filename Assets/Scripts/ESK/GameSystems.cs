using FFS.Libraries.StaticEcs;

public struct GameSystemsType : ISystemsType
{
}

public abstract class GameSystems : GameWorld.Systems<GameSystemsType>
{
}