using UnityEngine;

public class TurretDamage : MonoBehaviour
{
    [SerializeField] private int _attackDamage = 3;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackInterval = 1f;
    
    private MonsterHealth[] monsters;
    private float _nextAttackTime;

    private void Start()
    {
        monsters = FindObjectsByType<MonsterHealth>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        checkMonsters();
    }

    private void checkMonsters()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            MonsterHealth monster = monsters[i]; 
            
            Vector3 direction = monster.transform.position - transform.position;
            float distance = direction.magnitude;
            
            if (distance <= _attackRange)
            {
                if (Time.time >= _nextAttackTime)
                {
                    monster.TakeDamage(_attackDamage);
                    _nextAttackTime = Time.time + _attackInterval;
                }
            }
        }
    }
    
    
}
