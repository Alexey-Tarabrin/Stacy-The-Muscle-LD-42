using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] EnemyPrefab;
    public Transform YSpawnPos;
    [SerializeField] [Range(0f, 5f)] private float _secondsBetweenSpawn = 1;
    [SerializeField] private float _maxDistanceFromCenter = 5f;
    public List<EnemyManager> DisabledEnemies;

    private void Awake()
    {
        DisabledEnemies = new List<EnemyManager>();
    }

    private void Start()
    {
        StartCoroutine(InitSpawn());
    }

    private IEnumerator InitSpawn()
    {
        while (GameDataManager.Instance.PlayerManager.PlayerAttributes.Condition == PlayerCondition.Alive)
        {
            yield return new WaitForSeconds(_secondsBetweenSpawn);
            if (DisabledEnemies.Count > 0)
            {
                var curr = DisabledEnemies[0];
                curr.gameObject.SetActive(true);
                curr.transform.position = new Vector3(Random.Range(-_maxDistanceFromCenter, _maxDistanceFromCenter),
                    YSpawnPos.position.y,
                    10);
                DisabledEnemies.RemoveAt(0);
            }
            else
            {
                InstantiateEnemyFromPrefab();
            }
        }
    }

    private void InstantiateEnemyFromPrefab()
    {
        var enemy = Instantiate(EnemyPrefab[Random.Range(0, EnemyPrefab.Length)],
            new Vector3(Random.Range(-_maxDistanceFromCenter, _maxDistanceFromCenter), YSpawnPos.position.y,
                10),
            Quaternion.identity, transform);
        var manager = enemy.GetComponent<EnemyManager>();
        manager.SetManager(this);
    }
}