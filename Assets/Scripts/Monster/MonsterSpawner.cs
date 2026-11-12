using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private MonsterMove monsterPrefab;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField]private float spawnInterval = 2f;
    
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnInterval)
        {
            spawnMonster();
            timer -= spawnInterval;
        }
    }

    private void spawnMonster()
    {
        MonsterMove monster = Instantiate(monsterPrefab, spawnPoint.position, spawnPoint.rotation);
        monster.Init(waypoints, endPoint);
    }
        
}
