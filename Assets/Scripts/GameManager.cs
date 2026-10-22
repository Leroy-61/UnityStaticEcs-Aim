using UnityEngine;
using FFS.Libraries.StaticEcs;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

public GameObject crosshair;
private TargetPool targetPool;
public GameObject startButton;
[SerializeField] private TargetCatalog targetCatalog;
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
        targetPool = new TargetPool();
        GameSystems.Create();
        GameSystems.Add(new TargetCountSystem());
        targetSpawnSystem = new TargetSpawnSystem(targetCatalog);
        GameSystems.Add(targetSpawnSystem);
        GameSystems.Add(new TargetPositionSystem());
        GameSystems.Add(new TargetViewCreationSystem(targetPool));
        GameSystems.Add(new TargetViewSystem());
        GameSystems.Add(new PlayerInputSystem(aimAction, shootAction));
        GameSystems.Add(new CrosshairViewSystem(crosshair));
        GameSystems.Add(new TargetHitSystem(targetPool));
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