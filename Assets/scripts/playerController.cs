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

    private PlayerInput _playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private InputAction _moveAction;
    private InputAction _JumpAction;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _playerInput= GetComponent<PlayerInput>();
       _moveAction =_playerInput.actions["Move"];
        _JumpAction = _playerInput.actions["Jump"];
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        controller.Move(move * Time.deltaTime * playerSpeed);


        // Changes the height position of the player..
        if (_JumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
