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
        Move(moveValue, moveSpeed);
        //Debug.Log(moveValue);
    }
    
    public void Move(Vector2 direction, float speed)
    {
        var rotation = transform.rotation;
        var angles = rotation.eulerAngles;
        angles.y = _camera.transform.rotation.eulerAngles.y;
        rotation.eulerAngles = angles;
        transform.rotation = rotation;
        float horizontal = direction.x;
        float vertical = direction.y;

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection.x = moveDirection.x * speed;
        moveDirection.y = _rigidbody.velocity.y;
        moveDirection.z = moveDirection.z * speed;

        _rigidbody.velocity = moveDirection;
    }
}
