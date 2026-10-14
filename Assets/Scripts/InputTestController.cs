using UnityEngine;
using UnityEngine.InputSystem;

public class InputTestController : MonoBehaviour

{
    public InputActionReference ShootAction;
    public InputActionReference AimAction;
    void OnShoot(InputAction.CallbackContext context)
        {
            Debug.Log("Выстрел!");
        }
    void OnEnable()
        {
            AimAction.action.Enable();
            ShootAction.action.Enable();
            ShootAction.action.performed += OnShoot;
        }
    void OnDisable()
        {
            ShootAction.action.Disable();
            AimAction.action.Disable();
            ShootAction.action.performed -= OnShoot;
        }
}