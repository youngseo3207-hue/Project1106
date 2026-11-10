using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _endPoint;

    public void MoveDestination(Transform destination)
    {
        _endPoint =  destination;
    }

    private void Update()
    {
        if (_endPoint == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(
            transform.position, 
            _endPoint.position, 
            _moveSpeed * Time.deltaTime);

        if (IsArrived(transform.position, _endPoint.position))
        {
            Destroy(gameObject);
        }
    }

    public bool IsArrived(
        Vector3 currentPosition, 
        Vector3 targetPosition, 
        float epsilon = 0.1f)
    {
        Vector3 direction = targetPosition - currentPosition;

        float sqrDistance = direction.sqrMagnitude;

        return sqrDistance <= (epsilon * epsilon);
    }
}
