using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bualteController : MonoBehaviour
{
    //[SerializeField] private GameObject bulatDecal;

    private float speed = 50f;
    private float _timetoDestroy = 3f;

    public Vector3 target{get;set;}
    public bool hit { get;set;}
    private void OnEnable()
    {
        Destroy(gameObject, _timetoDestroy);
    }

 
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position, target, speed);
        if(!hit && Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
