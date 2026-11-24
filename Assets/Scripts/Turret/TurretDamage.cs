using UnityEngine;

public class TurretDamage : MonoBehaviour
{
    [SerializeField] private float attackDamage = 3;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackInterval = 1f;
    
    private float nextAttackTime;

    private void Update()
    {
        if (Time.time < nextAttackTime)
            return;

        MonsterHealth target = FindMonster();
        
        if (target == null)
            return;

        target.TakeDamage((int)attackDamage);
        nextAttackTime = Time.time + attackInterval;
        
        Debug.Log($"몬스터 수: {FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None).Length}");
    }

    private MonsterHealth FindMonster()
    {
        MonsterHealth[] monsters = FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None);
        
        MonsterHealth closest = null;
        float closestDistSqr = attackRange * attackRange;

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
        
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
