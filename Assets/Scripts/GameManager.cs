using UnityEngine;
using FFS.Libraries.StaticEcs;

public class GameManager : MonoBehaviour
{

public GameObject crosshair;
public GameObject startButton;

void Start()
    {

    crosshair.SetActive(false);

        GameWorld.Create();
        GameWorld.Types().RegisterAll();
        GameWorld.Initialize();
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