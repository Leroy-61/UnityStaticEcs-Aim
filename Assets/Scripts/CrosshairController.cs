using UnityEngine;
using UnityEngine.InputSystem; 

public class CrosshairController : MonoBehaviour 
{
void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
void Update()
    {
    Vector2 mousePosition = Mouse.current.position.ReadValue();
    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    worldPosition.z = 0;
    transform.position = worldPosition;

    if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Shot!");
        }
    }
void OnDestroy()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
