using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    [SerializeField] private int _monsterMaxHealth = 10;
    
    private int _monsterCurrentHealth;

    private void Awake()
    {
        _monsterCurrentHealth = _monsterMaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _monsterCurrentHealth -= damage;
        Debug.Log("파괴");

        if (_monsterCurrentHealth <= 0)
        {
            _monsterCurrentHealth = 0;
            Destroy(gameObject);
        }
    }
}
