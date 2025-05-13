using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementRadius;
    private NavMeshAgent _agent;
    private Vector3 _destination;
    private Vector3 _defaultPosition;
    
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _defaultPosition = transform.position;
        StartCoroutine(RandomMovement());
    }
    
    private void SetRandomDestination()
    {
        _destination = _defaultPosition + Random.insideUnitSphere * movementRadius;
        _agent.SetDestination(_destination);
    }

    private IEnumerator RandomMovement()
    {
        while (true)
        {
            SetRandomDestination();
            yield return new WaitForSeconds(Random.Range(3, 7));
        }
        // ReSharper disable once IteratorNeverReturns
        // Thanks rider for these automatic suggestions lol
    }
}
