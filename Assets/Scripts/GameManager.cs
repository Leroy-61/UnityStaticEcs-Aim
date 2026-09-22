using UnityEngine;
using FFS.Libraries.StaticEcs;

public class GameManager : MonoBehaviour
{

public GameObject crosshair;
public GameObject startButton;
public GameObject targetView;
public TargetSpawnSystem targetSpawnSystem;

void Start()
    {


        GameWorld.Create();
        GameWorld.Types().RegisterAll();
        GameWorld.Initialize();
        GameSystems.Create();
        GameSystems.Add(new TargetViewSystem());
        targetSpawnSystem = new TargetSpawnSystem(targetView);
        GameSystems.Add(targetSpawnSystem);
        GameSystems.Add(new TargetHitSystem(crosshair,  targetView));
        GameSystems.Initialize();
    }

public void StartGame()
    {
    targetSpawnSystem.Spawn();
    startButton.SetActive(false);
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