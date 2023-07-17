using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QustPanal : MonoBehaviour
{
   private bool isQpress = false;
    [SerializeField] private GameObject Qustpanel;
    void Start()
    {
        
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            isQpress = !isQpress;
            Qustpanel.SetActive(isQpress);
           
        }
    }
}
