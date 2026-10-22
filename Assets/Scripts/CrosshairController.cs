using UnityEngine;

public class CrosshairController : MonoBehaviour 
{
void Start()
    {      
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
