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
    }

    private MonsterHealth FindMonster()
    {
        MonsterHealth[] monsters = FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None);
        
        MonsterHealth closest = null;
        float closestDistSqr = _attackRange * _attackRange;

        foreach (MonsterHealth monster in monsters)
        {
            float distSqr = (monster.transform.position - transform.position).sqrMagnitude;

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
