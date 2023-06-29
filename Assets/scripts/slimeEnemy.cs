using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeEnemy : MonoBehaviour
{
   [SerializeField] private Transform _playerTranform;
    [SerializeField] private float _playerToDistance = 20;

    private bool istriggerd=false;
    private NavMeshAgent _agent;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerTranform.position) <= _playerToDistance)
        {
;
            istriggerd = true;
        }

        if(istriggerd)
        {
            _agent.SetDestination(_playerTranform.position);
            Debug.Log("Trigered");
        }
    }
}
