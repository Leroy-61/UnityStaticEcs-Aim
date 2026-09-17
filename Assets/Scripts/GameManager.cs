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
        GameSystems.Initialize();
        GameWorld.Initialize();

    var targetEntity = GameWorld.NewEntity<Default>();

        targetEntity.Set(new Target());
        targetEntity.Set(new Position
            {
                Value = new Vector2(3, 5)
            });
        targetEntity.Set(new TargetView
            {
                Value = targetView
            });
    }

public void StartGame()
    {
    startButton.SetActive(false);
    crosshair.SetActive(true);
    }
void OnDestroy()
    {
        GameWorld.Destroy();
    }
}