using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bualteController : MonoBehaviour
{
    [SerializeField] private GameObject bulateFx;

    private float speed = 50f;
    private float _timetoDestroy =0.2f;

    public Vector3 target{get;set;}
    public bool hit { get;set;}


    IEnumerator blastCount() {

        yield return new WaitForSeconds(0.19f);
        Instantiate(bulateFx, transform.position, transform.rotation);

    }
    private void OnEnable()
    {
        StartCoroutine(blastCount());
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
        Instantiate(bulateFx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
