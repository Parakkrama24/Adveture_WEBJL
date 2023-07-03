using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField]private GameObject _chestOpenGameObj;
    private bool isColledChest;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isColledChest = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Chest");
            Instantiate(_chestOpenGameObj, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
