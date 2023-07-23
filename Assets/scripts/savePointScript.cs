using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class savePointScript : MonoBehaviour
{
  
    //[SerializeField] private Slider _helthBar;
    [SerializeField] private GameObject[] ckeckPonits;
   // [SerializeField] private playerController PlayerController;
    [SerializeField] private GameObject Player;




    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

   public void respwner()
    {
        Player.transform.position = ckeckPonits[0].transform.position;
        gameObject.SetActive(false);
        

       
        /*Debug.Log("Player");
        Debug.Log(PlayerController.CheckPontCount);
        if ( PlayerController.CheckPontCount != 0)
        {
            Debug.Log("Player2");
            for (int i = 0; i < ckeckPonits.Length; i++)
            {
                if (i+1== PlayerController.CheckPontCount)
                {

                    Player.transform.position = ckeckPonits[i].transform.position;
                    Instantiate(Player, ckeckPonits[i].transform.position,Quaternion.identity);
                    Debug.Log("Player3");
                    _helthBar.value = 200f;

                }
            }
        }*/
   }
}//class
