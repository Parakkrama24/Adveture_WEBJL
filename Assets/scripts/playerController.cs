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
    private float _animationAmoothTime=0.2f;
    [SerializeField]
    private float jumpAnimationPlaytransition = 0.15f;

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
        moveXAnimatoreId = Animator.StringToHash("MoveX");
        moveZAnimatoreId = Animator.StringToHash("MoveY");


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
            _animator.CrossFade(_jumpAnimation, jumpAnimationPlaytransition);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotate toward camera direction

        float targetAngle =_cameraTranform.rotation.eulerAngles.y;
        Quaternion target_rotation= Quaternion.Euler(0,targetAngle, 0);
        transform.rotation= Quaternion.Lerp(transform.rotation, target_rotation,_rotationSpeed*Time.deltaTime);
    }
}
