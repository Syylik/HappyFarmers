using UnityEngine;
using UnityEngine.InputSystem;

public class Input : MonoBehaviour
{
    private GameInputControler _input;

    [SerializeField] private LayerMask _touchMask;

    private void OnEnable()
    {
        _input ??= new GameInputControler();
        _input.Enable();

        _input.Gameplay.Touch.performed += OnTouch; 
    }

    private void OnTouch(InputAction.CallbackContext context)
    {
        var touchRay = Camera.main.ScreenPointToRay(_input.Gameplay.TouchPosition.ReadValue<Vector2>());
        if(Physics.Raycast(touchRay, out RaycastHit hit, 100f, _touchMask))
        {
            hit.collider.GetComponent<ICollectable>().Collect();
        }
    }

    private void OnDisable()
    {
        _input.Gameplay.Touch.performed -= OnTouch;

        _input.Disable();
    }
}
