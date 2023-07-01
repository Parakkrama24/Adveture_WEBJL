using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CamaraSwitch : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int _waypontIndex;
    private Vector3 _target;
    private Animator _animator;


    [SerializeField] private Transform[] _wayPointTransform;
    [SerializeField] private float _nearpotion = 0.4f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _playerToDistance = 5f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        updateDestination();
    }


    void Update()
    {
        if (Vector3.Distance(transform.position, _target) <= _nearpotion && !(Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance))
        { 
                itarateWayPoitIndex();
                updateDestination();
        }

        if(Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance)
        {
            if(Vector3.Distance(transform.position, _playerTransform.position) <= 5)
            {
                _animator.SetTrigger("punch");
            }
            //Debug.Log(Vector3.Distance(transform.position, _playerTransform.position));
            updatePlayerDestination();
        }
    }

    private void updatePlayerDestination()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    private void updateDestination()
    {
        _target = _wayPointTransform[_waypontIndex].position;
        _agent.SetDestination(_target);
    }
    private void itarateWayPoitIndex()
    {
        _waypontIndex++;
        if (_waypontIndex == _wayPointTransform.Length)
        {
            _waypontIndex = 0;
        }
    }
}
