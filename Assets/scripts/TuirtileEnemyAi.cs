using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class TuirtileEnemyAi : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int _waypontIndex=0;
    private Vector3 _target;
    private Animator _animator;
    private bool istriged;
    int _blastAnimation;
    private bool hasexploded=false;
    private float blastrad = 2.5f;
    private float blastForce = 500f;
    private SphereCollider _spearCollider;

    [SerializeField] private Transform[] _wayPointTransform;
    [SerializeField] private float _nearpotion = 0.4f;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _playerToDistance = 5f;
    [SerializeField] private GameObject _explotionFX;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        updateDestination();
        _spearCollider = GetComponent<SphereCollider>();
        _blastAnimation = Animator.StringToHash("turtuleblast");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _target) <= _nearpotion && !(Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance))
        {
           itarateWayPoitIndex();
           updateDestination();
        }
        if (Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance)
        {
            if (Vector3.Distance(transform.position, _playerTransform.position) <= 0.2 && !hasexploded)
            {
                exploded(); 
                
                hasexploded=true;
            }
            //Debug.Log(Vector3.Distance(transform.position, _playerTransform.position));
            
            istriged = true;
        }
        if (istriged && !hasexploded)
        {
            updatePlayerDestination();
        }


    }

    private void exploded()
    {
        Debug.Log("Blast");
        _animator.CrossFade(_blastAnimation, 0.01f);//shoot to player
        Instantiate(_explotionFX, transform.position, transform.rotation);
        _spearCollider.radius = 2.32f;
       /* Collider[] blastColliders = Physics.OverlapSphere(transform.position, blastrad);
       foreach (Collider nearbyObjects in blastColliders)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb != null)
            {
                
                rb.AddExplosionForce(blastForce, transform.position, blastrad);
            }
        }*/

       gameObject.SetActive(false);
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
            Debug.Log("called");
            _waypontIndex = 0;
        }
    }
}
