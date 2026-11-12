using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private int _monsterMaxHealth = 10;
    private MonsterData _monsterData;
    
    private int _monsterCurrentHealth;
    private float _damage;

    private void Awake()
    {
        _monsterCurrentHealth = _monsterMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _monsterCurrentHealth -= damage;

        if (_monsterCurrentHealth <= 0)
        {
            _monsterCurrentHealth = 0;
            Destroy(gameObject);
            
            Debug.Log("파괴");
        }
    }
}
