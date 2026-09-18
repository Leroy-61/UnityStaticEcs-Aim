using UnityEngine;
using FFS.Libraries.StaticEcs;

public class GameManager : MonoBehaviour
{

public GameObject crosshair;
public GameObject startButton;
public GameObject targetView;

void Start()
    {


        GameWorld.Create();
        GameWorld.Types().RegisterAll();
        GameSystems.Create();
        GameSystems.Add(new TargetViewSystem());
        var targetSpawnSystem = new TargetSpawnSystem(targetView);
        GameSystems.Add(targetSpawnSystem);
        GameSystems.Add(new TargetHitSystem(crosshair));
        GameSystems.Initialize();
        GameWorld.Initialize();
        targetSpawnSystem.Spawn();
    }

public void StartGame()
    {
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