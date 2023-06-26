using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class playerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]

    [Header("Aniamtion parameters")]
    private float _animationAmoothTime=0.2f;
    [SerializeField]
    private float jumpAnimationPlaytransition = 0.15f;
    [SerializeField]
    private float shootaniamationTransitionTime = 0.05f;

    private PlayerInput _playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform _cameraTranform;

    private InputAction _moveAction;
    private InputAction _JumpAction;
    private float _rotationSpeed=5f;

    private Animator _animator;
    int _jumpAnimation;
    int _bigJumpAnimation;
    int _attractAnimation;
    int  moveXAnimatoreId;
    int moveZAnimatoreId;

    private Vector2 _curruntAnimationBlendVector;
    private Vector2 _animationVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _playerInput= GetComponent<PlayerInput>();
        _cameraTranform=Camera.main.transform;
       _moveAction =_playerInput.actions["Move"];
        _JumpAction = _playerInput.actions["Jump"];

        _animator= GetComponent<Animator>();//getanimatore component to code
        _jumpAnimation = Animator.StringToHash("Jump");
        _bigJumpAnimation = Animator.StringToHash("BigJump");
        _attractAnimation = Animator.StringToHash("Attack");
        moveXAnimatoreId = Animator.StringToHash("MoveX");
        moveZAnimatoreId = Animator.StringToHash("MoveY");

        // Lock the cursor within the game window
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 8f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerSpeed = 5f;
        }

        if (Input.GetMouseButton(0))
        {
            _animator.CrossFade(_attractAnimation, shootaniamationTransitionTime);//play shoot animation
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.CrossFade(_bigJumpAnimation, jumpAnimationPlaytransition);//play jump animation
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursorLock();
        }


        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = _moveAction.ReadValue<Vector2>();
        _curruntAnimationBlendVector = Vector2.SmoothDamp(_curruntAnimationBlendVector, input, ref _animationVelocity, _animationAmoothTime);
        Vector3 move = new Vector3(_curruntAnimationBlendVector.x, 0, _curruntAnimationBlendVector.y);
        move= move.x*_cameraTranform.right.normalized+move.z*_cameraTranform.forward.normalized;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);
        _animator.SetFloat(moveXAnimatoreId, _curruntAnimationBlendVector.x);
        _animator.SetFloat(moveZAnimatoreId, _curruntAnimationBlendVector.y);

        // Changes the height position of the player..
        if (_JumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            //_animator.SetTrigger(_jumpAnimation);
            _animator.CrossFade(_jumpAnimation, jumpAnimationPlaytransition);//play jump animation
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotate toward camera direction

        float targetAngle =_cameraTranform.rotation.eulerAngles.y;
        Quaternion target_rotation= Quaternion.Euler(0,targetAngle, 0);
        transform.rotation= Quaternion.Lerp(transform.rotation, target_rotation,_rotationSpeed*Time.deltaTime);
    }

    private void ToggleCursorLock()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            // Show the cursor
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            // Lock the cursor within the game window
            Cursor.lockState = CursorLockMode.Locked;
            // Hide the cursor
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}
