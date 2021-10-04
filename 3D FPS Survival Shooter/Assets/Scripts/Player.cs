using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _doubleJump;
    private Vector3 _direction;
    private Vector3 _playerVelocity;
    [Header("Controller settings")]
    [SerializeField] private float _speed;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _gravityValue;
    [Header("Camera Settings")]
    [SerializeField] private Camera camera;
    [SerializeField] private float _cameraSensitivity;
    [SerializeField] private bool _invertY = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    private void FixedUpdate()
    {

    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


        //mouseX camera movement
        //rotate left and right Y-axis both the below do the same thing
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + mouseX, transform.localEulerAngles.z);
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);
        //transform.rotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //mouseY camera movement
        Vector3 currentCameraRotation = camera.gameObject.transform.localEulerAngles;
        if (_invertY)
        {
            currentCameraRotation.x += mouseY * _cameraSensitivity;
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0f , 21.4f);
        }
        else
        {
            currentCameraRotation.x -= mouseY * _cameraSensitivity;
            currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0f, 21.4f);
        }
        //currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 21.4f, 13f);

        camera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
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
        //changes the direction from world space to local space, so that the player can move where the camera is pointing
        _playerVelocity = transform.TransformDirection(_playerVelocity);
        _controller.Move(_playerVelocity * Time.deltaTime);
        
    }
}
