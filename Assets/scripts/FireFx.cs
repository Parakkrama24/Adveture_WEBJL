using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFx : MonoBehaviour
{
    [SerializeField] private Transform BulateTransform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = BulateTransform.position;
    }
}
