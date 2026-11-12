using UnityEngine;

public class TurretDamage : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 3;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackInterval = 1f;
    
    private float _nextAttackTime;

    private void Update()
    {
        if (Time.time < _nextAttackTime)
            return;

        MonsterHealth target = FindMonster();
        
        if (target == null)
            return;

        target.TakeDamage(_attackDamage);
        _nextAttackTime = Time.time + _attackInterval;
        
        Debug.Log($"몬스터 수: {FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None).Length}");
    }

    private MonsterHealth FindMonster()
    {
        MonsterHealth[] monsters = FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None);
        
        MonsterHealth closest = null;
        float closestDistSqr = _attackRange * _attackRange;

        foreach (MonsterHealth monster in monsters)
        {
            Vector3 diff = monster.transform.position - transform.position;
            diff.y = 0f;
            
            float distSqr = diff.sqrMagnitude;

            if (distSqr <= closestDistSqr)
            {
                closestDistSqr = distSqr;
                closest = monster;
            }
        }
        
        return closest;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
