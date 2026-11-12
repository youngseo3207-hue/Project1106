using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;

    private float spawnInterval = 2f;
    private float timer = 0f;
    
    private bool isSpawning = false;

    private void Start()
    {
        StartSpawning();
    }

    private void StartSpawning()
    {
        isSpawning = true;
    }

    private void Update()
    {
        if (!isSpawning)
        {
            return;
        }
        
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            spawnMonster();
            timer -= spawnInterval;
        }
    }

    private void spawnMonster()
    {
        GameObject monster = Instantiate(
            monsterPrefab,
            spawnPoint.position,
            spawnPoint.rotation
            );
        
        MonsterMove monsterMove = monster.GetComponent<MonsterMove>();

        if (monsterMove == null)
        {
            Debug.Log("컴포넌트 없음");
            return;
        }
        
        monsterMove.MoveDestination(endPoint);
    }
}
