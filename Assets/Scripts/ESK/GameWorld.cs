using FFS.Libraries.StaticEcs;

public struct GameWorldType : IWorldType
{
}

public abstract class GameWorld : World<GameWorldType>
{
}