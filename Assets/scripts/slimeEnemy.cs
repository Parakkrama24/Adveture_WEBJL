using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class slimeEnemy : MonoBehaviour
{
    [SerializeField] private float _playerToDistance = 20;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _explotionFX;
    [SerializeField] private Slider _playerSlider;
    [SerializeField] private float reduseValuve;

    private bool hasexploded;
    private bool istriggerd=false;
    private SphereCollider _spearCollider;
    private NavMeshAgent _agent;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _spearCollider = GetComponent<SphereCollider>();
        _spearCollider.radius = 2.32f;
        _spearCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, _playerTransform.position) <= _playerToDistance)
        {
            istriggerd = true;

        }
        

        if (istriggerd && !hasexploded)
        {
            _agent.SetDestination(_playerTransform.position);
        
            Debug.Log("Trigered");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            exploded();
        }
    }
    private void exploded()
    {
        Instantiate(_explotionFX, transform.position, transform.rotation);
        _playerSlider.value -= reduseValuve;
        hasexploded= true;
        Destroy(this.gameObject);
    }
}
