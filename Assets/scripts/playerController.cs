
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class playerController : MonoBehaviour
{
    [Header("Physics")]
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    [Header("Objects and Transform")]
    public GameObject _bulateFreeFab;
    [SerializeField]
    private Transform _weponTranform;
    [SerializeField]
    private Transform _perantTransform;
    [SerializeField]
    private TextMeshProUGUI _playerText;
    [SerializeField]
    private GameObject _ChestUiImage;

    [Header("Aniamtion parameters")]
    [SerializeField]
    private float _animationAmoothTime=0.2f;
    [SerializeField]
    private float jumpAnimationPlaytransition = 0.15f;
    [SerializeField]
    private float shootaniamationTransitionTime = 0.05f;

    [Header("Audio")]
    [SerializeField]private AudioClip _audioClipBlast;
    [SerializeField]private AudioClip _audioClipDead;
    [SerializeField]private AudioClip _audioClipChest;

    [Header("variables")]
    [SerializeField] private float uiScrolCloseTime = 180f;
    [SerializeField] private float helthmax = 200f;


    private AudioSource _audioSource;
    private PlayerInput _playerInput;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform _cameraTranform;

    private InputAction _moveAction;
    private InputAction _JumpAction;
    private InputAction _atrackAction;
    private float _rotationSpeed=5f;

    private Animator _animator;
    int _jumpAnimation;
    int _bigJumpAnimation;
    int _attractAnimation;
    int  moveXAnimatoreId;
    int moveZAnimatoreId;

    private Vector2 _curruntAnimationBlendVector;
    private Vector2 _animationVelocity;

    [Header("Ui")]
    public Slider _helthbar;


    [SerializeField] private Transform[] checkPoints;
    private Vector3 newSpwanPotion=new Vector3(0,0,0);
   // private savePointScript _savePoints;
    //[SerializeField] private TextMeshProUGUI _pointText;
   // private int _point=0;

    private void Awake()
    {
       // transform.position = Vector3.zero;
        _ChestUiImage.SetActive(false);
        controller = GetComponent<CharacterController>();
        _playerInput= GetComponent<PlayerInput>();
        _cameraTranform=Camera.main.transform;
       _moveAction =_playerInput.actions["Move"];
        _JumpAction = _playerInput.actions["Jump"];
        _atrackAction = _playerInput.actions["Shoot"];
        _playerText.text = " ";

        _animator = GetComponent<Animator>();//getanimatore component to code
        _jumpAnimation = Animator.StringToHash("Jump");
        _bigJumpAnimation = Animator.StringToHash("BigJump");
        _attractAnimation = Animator.StringToHash("Attack");
        moveXAnimatoreId = Animator.StringToHash("MoveX");
        moveZAnimatoreId = Animator.StringToHash("MoveY");

        // Lock the cursor within the game window
        Cursor.lockState = CursorLockMode.Locked;
        // Hide the cursor
        Cursor.visible = false;

        _audioSource = GetComponent<AudioSource>();

       // _savePoints =GetComponent<savePointScript>();
    }
    private void OnEnable()
    {
        _atrackAction.performed += _ => shoot();
    }
    private void  OnDisable()
    {
        _atrackAction.performed -= _ => shoot();
    }

    private void shoot()
    {
        RaycastHit hit;
        GameObject bullate = Instantiate(_bulateFreeFab, transform.position, Quaternion.identity, _perantTransform);
        bualteController bualteController = bullate.GetComponent<bualteController>();
        _audioSource.clip = _audioClipBlast;
        _audioSource.Play();
        if (Physics.Raycast(_cameraTranform.position,_cameraTranform.forward,out hit, Mathf.Infinity))
        {
            bualteController.target = hit.point;
            bualteController.hit = true;
        }
        else
        {
            bualteController.target = _cameraTranform.position + _cameraTranform.forward;
            bualteController.hit = false;
        }
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

        if(_helthbar.value<=0)
        {
            //_animator.SetBool("isDead", true);//dead animation
            _audioSource.clip = _audioClipDead;
            _audioSource.Play();
          // transform.position=Vector3.zero;
            loadMainMenu();
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

    private  void loadMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // transform.position = _savePoints.newSpwanPotion;
        // _helthbar.value = 100f;
      //  _helthbar.value = 200f;
     //   transform.position = newSpwanPotion;

        Debug.Log(newSpwanPotion);
        //SceneManager.LoadScene(0);
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

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            if (other.CompareTag("CheckPoint" + i.ToString()))
            {
              //  newSpwanPotion = other.transform.position;
                Debug.Log(newSpwanPotion);
            }
        }
        if (other.CompareTag("enemyHand"))
        {
            _helthbar.value -= 10;
        }
        else if (other.CompareTag("TutuleEnemy"))
        {
            _helthbar.value -= 10;
            _audioSource.clip = _audioClipBlast;
            _audioSource.Play();
        } 
        else if (other.CompareTag("Chest0"))
        {
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            
            //Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest1"))
        {
           // _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "1111";
          //  Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest2"))
        {
            //_ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "2222";
            //Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest3"))
        {
           // _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "3333";
           // Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest4"))
        {
          //  _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "4444";
           // Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest5"))
        {
           // _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "5555";
            //Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest6"))
        {
          //  _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "6666";
            //Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest7"))
        {
            //_ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "7777";
            Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest8"))
        {
          //  _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "8888";
            Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest9"))
        {
           // _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
            _playerText.text = "9999";
            //Invoke("textNull", 3f);
        } else if (other.CompareTag("Chest10"))
        {
           // _ChestUiImage.SetActive(true);
            _helthbar.value = helthmax;
            _audioSource.clip = _audioClipChest;
            _audioSource.Play();
           // Invoke("textNull", 3f);
            _playerText.text = "101010";
        }

        if (other.CompareTag("0"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("1"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("2"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("3"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("4"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("5"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("6"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("7"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("8"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("9"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "0000";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("10"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "00gfghb00";
            Invoke("textNull", uiScrolCloseTime);
        }
        else if (other.CompareTag("11"))
        {
            _ChestUiImage.SetActive(true);
            _playerText.text = "fs";
            Invoke("textNull", uiScrolCloseTime);
        }


    }

    private void textNull()
    {
        _ChestUiImage.SetActive(false);
        _playerText.text = " ";
    }
}
