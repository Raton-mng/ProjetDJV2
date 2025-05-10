using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Trainer
{
    [SerializeField] private float moveSpeed;
    private InputAction _moveAction;
    private Rigidbody _rigidbody;
    private Camera _camera;
    private bool _isMoving;
    private bool _touchingGround;

    private void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        CheckTouchingGround();
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        Move(moveValue, moveSpeed);
        if (moveValue != Vector2.zero)
        {
            _isMoving = true;
        }
        else
        {
            if (_isMoving && _touchingGround)
            {
                _rigidbody.velocity = Vector3.zero;//attempt to avoid weird behaviors when stopping on slopes
            }

            _isMoving = false;
        }
        //Debug.Log(moveValue);
    }

    private void CheckTouchingGround()
    {
        _touchingGround = Physics.Raycast(transform.position, Vector3.down, 1.1f);
        Debug.Log(_touchingGround);
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
