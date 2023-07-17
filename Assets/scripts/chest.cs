using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class chest : MonoBehaviour
{
    [SerializeField]private GameObject _chestOpenGameObj;
    [SerializeField] private GameObject[] _NUllobject;
   
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
            if ((gameObject.tag == "Chest0"))
            {
                Instantiate(_NUllobject[0], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest1"))
            {
                Instantiate(_NUllobject[1], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest2"))
            {
                Instantiate(_NUllobject[2], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest3"))
            {
                Instantiate(_NUllobject[3], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest4"))
            {
                Instantiate(_NUllobject[4], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest5"))
            {
                Instantiate(_NUllobject[5], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest6"))
            {
                Instantiate(_NUllobject[6], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest7"))
            {
                Instantiate(_NUllobject[7], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest8"))
            {
                Instantiate(_NUllobject[8], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest9"))
            {
                Instantiate(_NUllobject[9], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest10"))
            {
                Instantiate(_NUllobject[10], transform.position, transform.rotation);
            } if ((gameObject.tag == "Chest11"))
            {
                Instantiate(_NUllobject[11], transform.position, transform.rotation);
            }
        }
        
    }
}
