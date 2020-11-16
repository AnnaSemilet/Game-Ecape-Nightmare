using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CameraBobbing _cameraBob;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed;
    private float _gravity = -19.62f;
    private float _groundDistance = 0.4f;
    private float _xPos;
    private float _zPos;

    private bool _isGrounded;


    private Vector3 _velocity;


    void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, _groundDistance, groundMask);
        
        if(_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _xPos = Input.GetAxis("Horizontal");
        _zPos = Input.GetAxis("Vertical");

        Vector3 move = transform.right * _xPos + transform.forward * _zPos;
        
        controller.Move(move * speed * Time.deltaTime);

        _velocity.y += _gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || 
            Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _cameraBob.isWalking = true;
        }
        else
        {
            _cameraBob.isWalking = false;
        }
    }
}
