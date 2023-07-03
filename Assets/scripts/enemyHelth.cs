using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemyHelth : MonoBehaviour
{
    [SerializeField] private Slider _enemyhelthBar;
   // [SerializeField] private GameObject _enemy;
     private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(_enemyhelthBar.value<=0)
        {
            _animator.SetBool("dead", true);
            Invoke("enemydeadCounting", 1.5f);
        }
    }

    private void enemydeadCounting()
    {
         Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bulate"))
        {
            _enemyhelthBar.value -= 20f;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
        }
    }
}
