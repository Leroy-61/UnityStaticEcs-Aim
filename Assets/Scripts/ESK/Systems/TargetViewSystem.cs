using FFS.Libraries.StaticEcs;

public struct TargetViewSystem : ISystem
{
    public void Update()
    {
        GameWorld.Query().For(
            static (in Position position, in TargetView targetView) =>
            {
                targetView.Value.transform.position = position.Value;
            });
    }
}