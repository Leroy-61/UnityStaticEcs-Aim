using FFS.Libraries.StaticEcs;
using UnityEngine;
using UnityEngine.InputSystem;

public struct PlayerInputSystem : ISystem
{
    private InputActionReference aimAction;
    private InputActionReference shootAction;
    public PlayerInputSystem(InputActionReference aimAction,InputActionReference shootAction)
        {
            this.aimAction = aimAction;
            this.shootAction = shootAction;
        }   
    public void Init()
        {
            aimAction.action.Enable();
            shootAction.action.Enable();
        }
    public void Destroy()
        {
            aimAction.action.Disable();
            shootAction.action.Disable();            
        }
    public void Update()
        {   
            Vector2 mousePosition = aimAction.action.ReadValue<Vector2>();
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            GameWorld.Query().For(
                (GameWorld.Entity entity, in Crosshair crosshair, in Position position) =>
                {
                    entity.Set(new Position { Value = worldMousePosition });
                });
            if (shootAction.action.WasPressedThisFrame())
            {
                GameWorld.Query().For(
            (GameWorld.Entity entity, in Crosshair crosshair, in Position position)=>
            {
                entity.Set(new ShootEvent());
            });
            }
        }
}