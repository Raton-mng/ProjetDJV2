using System.Collections.Generic;
using Items;
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
    public bool inTest;
    public Transform respawnPoint;

    private void Start()
    {
        //respawnPoint = transform;// purely to avoid null reference exception
        Cursor.visible = false;
        _moveAction = InputSystem.actions.FindAction("Move");
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }
    
    public void Respawn()
    {
        transform.position = respawnPoint.position;
        _rigidbody.velocity = Vector3.zero;
    }

    public void AddNewPokemon(Pokemon pokemon)
    {
        if (instantiatedParty.Count >= 6)
        {
            print("Can't Add, PC not implemented yet");
            return;
        }
        
        instantiatedParty.Add(pokemon);
    }

    public void AddItems(Dictionary<PokeItem, int> rewards)
    {
        foreach (var item in rewards)
        {
            PokeItem itemType = item.Key;
            int itemAmount = item.Value;
            
            if (items.ContainsKey(itemType))
                items[itemType] += itemAmount;
            else
                items.Add(itemType, itemAmount);
        }

        foreach (var item in items)
        {
            print(item);
        }
    }
    
    private void FixedUpdate()
    {
        CheckTouchingGround();
        Vector2 moveValue = _moveAction.ReadValue<Vector2>();
        if(!inTest) Move(moveValue, moveSpeed);//Bad practice, but didn't find a workaround
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
        //Debug.Log(_touchingGround);
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
