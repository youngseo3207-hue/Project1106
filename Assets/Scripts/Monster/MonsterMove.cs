using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] public Transform[] _waypoints;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _moveSpeed = 3f;

    private float _ArriveDistance = 0.1f;
    private int _currentWaypoint = 0;
    
    public void Init(Transform[] waypoints,  Transform endPoint)
    {
        _waypoints = waypoints;
        _endPoint = endPoint;
        _currentWaypoint = 0;
    }
    
    private void Update()
    {
        Transform target = GetCurrentTarget();
        if (target == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position, 
            target.position, 
            _moveSpeed * Time.deltaTime
            );

        if (Vector3.Distance(transform.position, target.position) > _ArriveDistance)
            return;

        if (_waypoints != null && _currentWaypoint < _waypoints.Length)
        {
            _currentWaypoint++;
            return;
        }

        ReachBase();
    }

    private Transform GetCurrentTarget()
    {
        if (_waypoints != null && _currentWaypoint < _waypoints.Length) return _waypoints[_currentWaypoint];

        return _endPoint;
    }
    
    private void ReachBase()
    {
        Destroy(gameObject);
    }
}
