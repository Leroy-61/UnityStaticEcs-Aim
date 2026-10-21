using UnityEngine;
using FFS.Libraries.StaticEcs;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

public GameObject crosshair;
public GameObject startButton;
public GameObject targetView;
public TargetSpawnSystem targetSpawnSystem;
private GameWorld.Entity crosshairEntity;
private GameWorld.Entity gameStateEntity;
public InputActionReference aimAction;
public InputActionReference shootAction;

void Start()
    {


        GameWorld.Create();
        GameWorld.Types().RegisterAll();
        GameWorld.Initialize();
        crosshairEntity = GameWorld.NewEntity<Default>();
        gameStateEntity = GameWorld.NewEntity<Default>();
        crosshairEntity.Set(new Crosshair());
        crosshairEntity.Set(new Position { Value = Vector2.zero });
        GameSystems.Create();
        GameSystems.Add(new TargetViewSystem());
        targetSpawnSystem = new TargetSpawnSystem(targetView);
        GameSystems.Add(targetSpawnSystem);
        GameSystems.Add(new PlayerInputSystem(aimAction, shootAction));
        GameSystems.Add(new CrosshairViewSystem(crosshair));
        GameSystems.Add(new TargetHitSystem(crosshair,  targetView));
        GameSystems.Initialize();
        
    }

public void StartGame()
    {
    startButton.SetActive(false);
    gameStateEntity.Set(new GameStarted());
    crosshair.SetActive(true);
    }
void Update()
    {
        GameSystems.Update();
    }
void OnDestroy()
    {
        GameWorld.Destroy();
    }
}