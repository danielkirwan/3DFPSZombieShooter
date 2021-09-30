using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _controller;
    private Vector3 _playerVelocity;
    //private bool _groundedPlayer;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityValue;
    private bool _doubleJump;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }


    private void FixedUpdate()
    {

    }

    private void PlayerMovement()
    {

        //check if the player is grounded with the character controller
        if (_controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            _direction = new Vector3(horizontal, 0, vertical);
            _playerVelocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //_doubleJump = true;
                _playerVelocity.y = _jumpHeight;
                //if (!_controller.isGrounded && _doubleJump)
                //{
                //    _doubleJump = false;
                //    _playerVelocity.y = _jumpHeight;
                //}
            }
        }


        _playerVelocity.y -= _gravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
        
    }
}
