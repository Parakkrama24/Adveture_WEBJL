using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bulateScript : MonoBehaviour
{
    private Transform PlayerTransform;
    private float moveSpeed = 50;
    private float timer = 0;
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
      //  Invoke("MOveToPlayer", 0.1f);
    }


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >=2.5f)
        {
            Destroy(this.gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, PlayerTransform.position, moveSpeed * Time.deltaTime);

    }
    private void MOveToPlayer()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("BulateAndPlayer");
        }
    }
}

   