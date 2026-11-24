using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] public Transform[] waypoints;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _moveSpeed = 3f;

    private float arriveDistance = 0.1f;
    private int currentWaypoint = 0;
    
    public void Init(Transform[] waypoints,  Transform endPoint)
    {
        waypoints = waypoints;
        _endPoint = endPoint;
        currentWaypoint = 0;
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

        if (Vector3.Distance(transform.position, target.position) > arriveDistance)
            return;

        if (waypoints != null && currentWaypoint < waypoints.Length)
        {
            currentWaypoint++;
            return;
        }

        ReachBase();
    }

    private Transform GetCurrentTarget()
    {
        if (waypoints != null && currentWaypoint < waypoints.Length) return waypoints[currentWaypoint];

        return _endPoint;
    }
    
    private void ReachBase()
    {
        Destroy(gameObject);
    }
}
