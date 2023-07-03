using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCntroller : MonoBehaviour
{
    [SerializeField]private  TMP_Text m_Text;

    private int _points;
    void Start()
    {
        m_Text.text = "Coins";
        _points = 0;
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
           _points=Random.Range(0, 10);
            m_Text.text= "Coins : "+_points.ToString();
        }
    }
}
