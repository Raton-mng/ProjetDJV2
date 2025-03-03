using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Trainer
{
    [SerializeField] float moveSpeed;
    private InputAction _moveAction;
    private Rigidbody _rigidbody;
    private Camera _camera;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        transform.Translate(moveValue * (Time.deltaTime * moveSpeed));
    }
}
