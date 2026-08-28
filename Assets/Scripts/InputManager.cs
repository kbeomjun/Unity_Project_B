using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputActions _inputActions;

    public InputActions.GamePlayActions GamePlay => _inputActions.GamePlay;

    public static InputManager Instance;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _inputActions = new InputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

}
