using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class chestEnemy : MonoBehaviour
{

    [Header("game objects and trasform")]
    [SerializeField] private float _playerToDistance = 20;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private Slider mySlider;


    private NavMeshAgent _agent;
    private Animator _animator;
    private bool istriggerd;
    private bool Isinstance;
   // [SerializeField] private GameObject _bulateFreefab;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (_playerTransform != null)
            if (Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance)
            {
                istriggerd = true;
                _animator.SetBool("trgeerd", true);

            }
            else
            {
                _animator.SetBool("trgeerd", false);
                istriggerd = false;
            }

        if (istriggerd)
        {
            _agent.SetDestination(_playerTransform.position);
            // InvokeRepeating("spawner", 1f, 1f);
            Isinstance = true;


        }
        if (mySlider.value <= 0f)
        {
            Destroy(gameObject);
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bulate"))
        {
            mySlider.value -= 50f;

        }
    }


}
