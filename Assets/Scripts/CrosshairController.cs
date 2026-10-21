using UnityEngine;
using UnityEngine.InputSystem; 

public class CrosshairController : MonoBehaviour 
{
void Start()
    {
        Debug.Log("CROSSHAIR START!");
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
void Update()
    {
    }
void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
