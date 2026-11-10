using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform monsterPoint;
    [SerializeField] private Transform _endPoint;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            MonsterSpawn();
        }
    }

    private void MonsterSpawn()
    {
        if (monsterPrefab != null)
        {
            Vector3 spawnPosition = monsterPoint.position;

            GameObject monster = Instantiate(
                monsterPrefab, spawnPosition, Quaternion.identity);
            
            MonsterMove monsterMove = monster.GetComponent<MonsterMove>();
            monsterMove.MoveDestination(_endPoint);
        }
    }
}
