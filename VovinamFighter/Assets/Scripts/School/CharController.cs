using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharController : MonoBehaviour
{
    [Header("Player setting")]
    [SerializeField]
    private float walkingSpeed = 7.5f;

    [SerializeField] private float runningSpeed = 11.5f;
    [SerializeField] private float jumpSpeed = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera playerCamera2;
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float lookXLimit = 45.0f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator playerAnimator;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;

    private bool _canMove = true;
    private bool _isPause = false;
    private bool _isJump = false;


    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        if (_isPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
                return;
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            return;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            SwitchCamera();
        }

        Vector3 forward = characterController.transform.TransformDirection(Vector3.forward);
        Vector3 right = characterController.transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = _canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = _canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && _canMove && characterController.isGrounded)
        {
            playerAnimator.SetBool("Run", false);
            playerAnimator.SetBool("WalkToward", false);
            playerAnimator.SetBool("WalkBack", false);
            playerAnimator.SetBool("Jump", true);
            _moveDirection.y = jumpSpeed;
            _isJump = true;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
            playerAnimator.SetBool("Jump", false);
            _isJump = false;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice 
        if (!characterController.isGrounded)
        {
            _moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(_moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            playerCamera2.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            characterController.transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            if (_isJump)
            {
                playerAnimator.SetBool("Jump", true);
                playerAnimator.SetBool("Run", false);
                playerAnimator.SetBool("WalkToward", false);
                playerAnimator.SetBool("WalkBack", false);
            }
            else if (curSpeedX > 0f || curSpeedY > 0f)
            {
                if (isRunning)
                {
                    playerAnimator.SetBool("WalkToward", false);
                    playerAnimator.SetBool("Run", true);
                }
                else
                {
                    playerAnimator.SetBool("WalkToward", true);
                    playerAnimator.SetBool("Run", false);
                }
            }
            else if (curSpeedX < 0f || curSpeedY < 0f)
            {
                if (isRunning)
                {
                    playerAnimator.SetBool("WalkBack", false);
                    playerAnimator.SetBool("Run", true);
                }
                else
                {
                    playerAnimator.SetBool("WalkBack", true);
                    playerAnimator.SetBool("Run", false);
                }
            }
            else
            {
                playerAnimator.SetBool("Run", false);
                playerAnimator.SetBool("WalkToward", false);
                playerAnimator.SetBool("WalkBack", false);
            }
        }
    }


    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _canMove = false;
        _isPause = true;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _canMove = true;
        _isPause = false;
    }

    private void SwitchCamera()
    {
        if (playerCamera.gameObject.activeInHierarchy)
        {
            playerCamera.gameObject.SetActive(false);
            playerCamera2.gameObject.SetActive(true);
        }
        else
        {
            playerCamera.gameObject.SetActive(true);
            playerCamera2.gameObject.SetActive(false);
        }
    }

    private void ResetAnim()
    {
        playerAnimator.SetBool("Jump", false);
        playerAnimator.SetBool("Run", false);
        playerAnimator.SetBool("WalkToward", false);
        playerAnimator.SetBool("WalkBack", false);
    }

  
    
}